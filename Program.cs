using System;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;


namespace Idtm {
    class Program {
        

        static JsonSchema schema = JsonSchema.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\docs\\schema.json"));

        static void Main(string[] args) {
            string file;
            if(args.Length == 1 && File.Exists(args[0])){
                file = args[0];
            }else if(args.Length != 0){
                Console.WriteLine("The file you tried to specify doesn't exist");
            }

            JObject user = JObject.Parse(@"{
                'IMG_0001.jpg':{'tag1':5, 'tag2':7},
                'IMG_0002.jpg':{'tag1':4, 'tag2':3},
                }");

                

            Console.WriteLine(user.IsValid(schema));
            
            





            Console.ReadKey();
        }
    }
}
