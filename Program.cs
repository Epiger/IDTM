using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Eto.Forms;
using System.Reflection;
using Idtm.IO;
using System.Collections.Generic;
using Idtm.Wind;


namespace Idtm {
    class Program {
        

        public static IDTMForm mainWindow;


        [STAThread]
        static void Main(string[] args) {
            string file;
            if(args.Length == 1 && File.Exists(args[0])){
                file = args[0];
            }else if(args.Length != 0){
                Console.WriteLine("The file you tried to specify doesn't exist");
            }

            //JObject user = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\docs\\demo.json"));
            //Console.WriteLine(user.IsValid(schema));

            Console.WriteLine(Path.GetDirectoryName("C:\\Some\\Path\\file.json") + Path.DirectorySeparatorChar + "imageFile");
            

            Application app = new Application();
            mainWindow = new IDTMForm();
            app.Run(mainWindow);
            
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "idtm.docs.schema.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)){
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }

            //Outputs the imgs NO VALIDATION TILL NOW
            foreach(Img img in Bio.ReadFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json")){
                Console.WriteLine(img.name);
                for(int i = 0; i < img.names.Count; i++){
                    Console.WriteLine("  " + img.names[i]);
                    Console.WriteLine("    " + img.values[i]);
                }
            }

    	    //Read and Save test
            Console.WriteLine(Bio.SaveFile(Bio.ReadFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json"), Directory.GetCurrentDirectory() + "\\docs\\test.json"));

            //Validation test
            Console.WriteLine(Bio.Validate(Directory.GetCurrentDirectory() + "\\demo\\demo.json"));

            Aio.OpenFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json");


            

            





            Console.ReadKey();
        }
    }

    

}
