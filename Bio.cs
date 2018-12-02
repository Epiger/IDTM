using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Idtm.IO {

    public class Bio {

        public static int actual = 0;
        public static string file = "";
        public static string dir = "";
        public static List<ITL> iTLs = new List<ITL>();
        public static List<string> header = new List<string>();
        public static string[] formats = new string[]{"jpg", "jpeg", "png", "tif", "tiff", "gif", "bmp"};


        public static void init(){
            //Call Aio.init() and Tio.init()
        }

        //Not tested
        public static void Save(string path){
            using(StreamWriter sw = new StreamWriter(path))
            using(JsonWriter jw = new JsonTextWriter(sw)){
                jw.Formatting = Formatting.Indented;

                //Root
                jw.WriteStartObject();

                //Write Header
                jw.WriteStartArray();
                jw.WritePropertyName("HEADER");
                foreach(string tag in header){
                    jw.WriteValue(tag);
                }
                jw.WriteEndArray();

                //Itls
                foreach(ITL itl in iTLs){
                    jw.WriteStartArray();
                    jw.WritePropertyName(itl.name);
                    foreach(int value in itl.values){
                        jw.WriteValue(value);
                    }
                    jw.WriteEndArray();
                }

                
                //Root end
                jw.WriteEndObject();

            }
        }

        public static void Open(string path){
            if(!Bio.Validate(path)){
                //Display message 
                Console.WriteLine("Schema doesnt match");
                return;
            }

             //Clear things up
            header.Clear();
            iTLs.Clear();

            //Update Variables
            dir = Path.GetDirectoryName(path);
            file = Path.GetFileName(path);

            using(JsonTextReader  reader = new JsonTextReader(new StreamReader(path))){
                
                //Temp storage
                string tempname = "";
                ITL tempitl = new ITL();
                

                while(reader.Read()){
                    if (reader.Value != null){
                        //Console.WriteLine("{0}, Value: {1}", reader.TokenType, reader.Value);
                        switch(reader.TokenType.ToString()){
                            case "PropertyName":
                                tempname = reader.Value.ToString();
                                break;
                            case "String":
                                if(tempname == "HEADER"){
                                    header.Add(reader.Value.ToString());
                                }
                                break;
                            case "Integer":
                                if(RightsExt(tempname)){
                                    tempitl.values.Add(Int32.Parse(reader.Value.ToString()));
                                }
                                break;
                        }
                    }else {
                        //Console.WriteLine("Token: {0}", reader.TokenType);
                        switch(reader.TokenType.ToString()){
                            case "EndArray":
                                if(RightsExt(tempname)){
                                    tempitl.name = tempname;
                                    iTLs.Add(tempitl);
                                    tempname = "";
                                }
                                break;
                            case "StartArray":
                                if(RightsExt(tempname)){
                                    tempitl = new ITL();
                                }
                                break;
                        }
                    }
                }

            }

            //Look for new images
            //Redraw the screen


                
        }


        public static void Create(string path){
            using(StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("idtm.docs.template.json")))
            using(StreamWriter sw = new StreamWriter(path)){
                sw.Write(sr.ReadToEnd());
            }
            //Open File afterwards
        }

        public static void Reload(){
            if(!(Bio.dir == "" || Bio.dir == null) && !(Bio.file == "" || Bio.file == null)){
                Open(Bio.dir + Path.DirectorySeparatorChar + Bio.file);
            }
        }


        //IT WOOORKS
        public static bool Validate(string path){
            using(StreamReader scr = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("docs.schema.json"))){
                JSchema schema = JSchema.Parse(scr.ReadToEnd());
                JObject file = JObject.Parse(File.ReadAllText(path));
                return file.IsValid(schema);
            }
        }

        public static bool RightsExt(string name){
            foreach(string ext in Bio.formats){
                if(name.Substring(name.LastIndexOf('.')+1).Equals(ext, StringComparison.InvariantCultureIgnoreCase)){
                    return true;
                }
            }
            return false;
        }


        public static List<string> GetFiles(string path){
            List<string> files = new List<string>();
            foreach(string file in Directory.GetFiles(path)){
                if(RightsExt(file)){
                    files.Add(Path.GetFileName(file));
                }
            }
            return files;
        }

       
    }


    public class ITL {

        public List<int> values = new List<int>();

        public string name{get; set;}

        
    }
    
}