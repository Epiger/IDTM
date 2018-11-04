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

            using(JsonTextReader  reader = new JsonTextReader(new StreamReader(path))){

                string tempname = "";
                ITL tempitl = new ITL();
                

                while(reader.Read()){
                    /*  Output
                        Token: StartObject
                        PropertyName, Value: HEADER
                        Token: StartArray
                        String, Value: tag1
                        String, Value: tag3
                        String, Value: tag4
                        Token: EndArray
                        PropertyName, Value: IMG_0067.jpg
                        Token: StartArray
                        Integer, Value: 5
                        Integer, Value: 7
                        Token: EndArray
                        PropertyName, Value: IMG_0096.jpg
                        Token: StartArray
                        Integer, Value: 4
                        Integer, Value: 3
                        Integer, Value: 1
                        Token: EndArray
                        Token: EndObject
                        */
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

                
        }

        //REEEMOVE
/*        public static List<Img> ReadFile(string path){
            //The Reader
            JsonTextReader reader = new JsonTextReader(new StreamReader(path));
            //The List
            List<Img> imgs = new List<Img>();
            //Reseting the tagHeader
            tagHeader.Clear();

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
                                //If object/array this string belongs to is named tag_header
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
                        case "EndArray":
                            //Reset img name
                            name = "";
                            break;
                    }
                }
            }

        return imgs;

        }
        */

        //IT WOOORKS ???
        public static bool Validate(string path){
            using(StreamReader scr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("idtm.docs.schema.json"))){
                JSchema schema = JSchema.Parse(scr.ReadToEnd());
                JObject file = JObject.Parse(File.ReadAllText(path));
                return file.IsValid(schema);
            }
        }

        //REEEEMOVE
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

        //REEEEMOVE
        public static bool CreateFile(string path){
            using(StreamWriter sw = new StreamWriter(path)){
                sw.WriteLine("{}");
            }
            return true;
        }

        public static bool RightsExt(string name){
            foreach(string ext in Bio.formats){
                if(name.Substring(name.LastIndexOf('.')+1).Equals(ext, StringComparison.InvariantCultureIgnoreCase)){
                    return true;
                }
            }
            return false;
        }

       
    }


    public class ITL {

        public List<int> values = new List<int>();

        public string name{get; set;}

        
    }
    


    //REMOOOVE
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