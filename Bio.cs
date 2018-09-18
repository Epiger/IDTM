using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Idtm.IO {

    public class Bio {

        public static List<Img> ReadFile(){
            JsonTextReader reader = new JsonTextReader(new StreamReader(Directory.GetCurrentDirectory() + "\\docs\\demo.json"));
            while (reader.Read()){
            if (reader.Value != null){
                Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            }else{
                Console.WriteLine("Token: {0}", reader.TokenType);
            }
        }

        return null;

        }
    }


    public class Img {

        public List<int> values;
        
        public List<string> names;

        string name;

    }

}