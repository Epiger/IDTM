using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Eto.Forms;


namespace Idtm {
    class Program {
        

        public static JSchema schema = JSchema.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\docs\\schema.json"));

        static void Main(string[] args) {
            string file;
            if(args.Length == 1 && File.Exists(args[0])){
                file = args[0];
            }else if(args.Length != 0){
                Console.WriteLine("The file you tried to specify doesn't exist");
            }

            //JObject user = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\docs\\demo.json"));
            //Console.WriteLine(user.IsValid(schema));

            Application app = new Application();
            app.Run(new IDTMForm());
            
            
            





            Console.ReadKey();
        }
    }

    

}
