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

        public static string idtmFile = "";
        public static string idtmFolder = "";
        public static List<Img> imgs = new List<Img>();
        public static List<string> filesInFolder = new List<string>();
        public static string[] formats = new string[]{"jpg", "jpeg", "png", "tif", "tiff", "gif", "bmp"};
        public static List<string> tagHeader = new List<string>();

        public static List<Img> ReadFile(string path){
            //The Reader
            JsonTextReader reader = new JsonTextReader(new StreamReader(path));
            //The List
            List<Img> imgs = new List<Img>();

            //Just to skip first
            bool inRoot = false;
            //A buffer for a var name
            string name = "";
            //List counter
            int imgI = -1;
            while (reader.Read()){
                if (reader.Value != null){
                    Console.WriteLine("{0}, Value: {1}", reader.TokenType, reader.Value);
                    switch(reader.TokenType.ToString()){
                        case "PropertyName":
                            if(name == ""){
                                //If this is an image name
                                name = reader.Value.ToString();
                            }else {
                                //If this is an Integer name
                                imgs[imgI].names.Add(reader.Value.ToString());
                            }
                            break;
                        case "Integer":
                            //Sets value of the Integer
                            imgs[imgI].values.Add(Int32.Parse(reader.Value.ToString()));
                            break;
                        case "String":
                            if(name == "tag_header"){
                                tagHeader.Add(reader.Value.ToString());
                            }
                            break;
                    }
                }else{
                    Console.WriteLine("Token: {0}", reader.TokenType);
                    switch(reader.TokenType.ToString()){
                        case "StartObject":
                            if(inRoot){
                                //if we are already in the root object the following object has to be an img
                                imgs.Add(new Img(name));
                                imgI++;
                            }else {
                                //Now we are in the root
                                inRoot = true;
                            }
                            break;
                        case "EndObject":
                            //Reset img name
                            name = "";
                            break;
                        case "StartArray":
                            
                            break;
                        case "EndArray":
                            name = "";
                            break;
                    }
                }
            }

        return imgs;

        }

        public static bool Validate(string path){
            using(StreamReader scr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("idtm.docs.schema.json"))){
                JSchema schema = JSchema.Parse(scr.ReadToEnd());
                JObject file = JObject.Parse(File.ReadAllText(path));
                return file.IsValid(schema);
            }
        }


        public static bool SaveFile(List<Img> imgs, string path){
            using(StreamWriter sw = new StreamWriter(path))
            using(JsonWriter jw = new JsonTextWriter(sw)){
                jw.Formatting = Formatting.Indented;

                jw.WriteStartObject();

                foreach(Img img in imgs){
                    jw.WritePropertyName(img.name);
                    jw.WriteStartObject();
                    for(int i = 0; i < img.names.Count; i++){
                        jw.WritePropertyName(img.names[i]);
                        jw.WriteValue(img.values[i]);
                    }
                    jw.WriteEndObject();
                }
                jw.WriteEndObject();

                //Writes the Text
                sw.WriteLine();

                return true;
            }

        

        }


        public static bool CreateFile(string path){
            using(StreamWriter sw = new StreamWriter(path)){
                sw.WriteLine("{}");
            }
            return true;
        }

        public static bool MatchesExt(string name){
            foreach(string ext in Bio.formats){
                if(name.Substring(name.LastIndexOf('.')+1).Equals(ext, StringComparison.InvariantCultureIgnoreCase)){
                    return true;
                }
            }
            return false;
        }

       
    }

    



    public class Img {
        
        //Stores the values corrosponding to the names
        public List<int> values = new List<int>();
        
        //Stores the names of all the values
        public List<string> names = new List<string>();

        //stores the name of the image
        public string name;

        public Img(string name){
            this.name = name;
        }

        public static int IndexOf(string name, List<Img> imgs){
            for(int i = 0; i < imgs.Count; i++){
                if(imgs[i].name == name){
                    return i;
                }
            }
            return -1;
        }

    }

}