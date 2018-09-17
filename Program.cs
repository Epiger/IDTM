using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Eto.Forms;
using System.Reflection;


namespace Idtm {
    class Program {
        

        //public static JSchema schema = JSchema.Parse(File.ReadAllText("docs\\schema.json"));

        public static IDTMForm mainWindow;

        public static bool projSaved = true;
        public static string actualFile = "";

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

            

            





            Console.ReadKey();
        }
    }

    

}
