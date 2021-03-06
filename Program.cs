﻿using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Eto.Forms;
using System.Reflection;
using Idtm.IO;
using System.Collections.Generic;
using Idtm.Wind;
using Eto.Wpf;
using System.Resources;


namespace Idtm {
    class Program {
        

        public static IDTMForm mainWindow;
        public static IDForm mainForm;


        [STAThread]
        static void Main(string[] args) {

            //Something older
            string file;
            if(args.Length == 1 && File.Exists(args[0])){
                file = args[0];
            }else if(args.Length != 0){
                Console.WriteLine("The file you tried to specify doesn't exist");
            }

            //IDK
            Console.WriteLine(Path.GetDirectoryName("C:\\Some\\Path\\file.json") + Path.DirectorySeparatorChar + "imageFile");


            //All the new things XsAML
            Application app = new Application(new Eto.Wpf.Platform());
            mainForm = new IDForm();
            app.Run(mainForm);



            //Some uld thungs
            var assembly = Assembly.GetEntryAssembly();
            string resourceName = "docs.schema.json";
            

            string[] rName = assembly.GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)){
            using (StreamReader reader = new StreamReader(stream)){
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }}

            //new bio test
            Bio.Open(Directory.GetCurrentDirectory() + "\\docs\\exam.json");
            foreach(ITL itl in Bio.iTLs){
                Console.WriteLine("Name: {0}", itl.name);
                for(int i = 0; i < itl.values.Count; i++){
                    Console.WriteLine("  {0}: {1}", Bio.header[i], itl.values[i]);
                }
            }
            Console.WriteLine("Path: {0}; File: {1}", Bio.dir, Bio.file);

            foreach(string filese in Bio.GetFiles(Directory.GetCurrentDirectory() + "\\demo")){
                Console.WriteLine(filese);
            }


            /* NEW
            
            string file;
            if(args.Length == 1 && File.Exists(args[0])){
                file = args[0];
            }else if(args.Length != 0){
                Console.WriteLine("The file you tried to specify doesn't exist");
            }

            //JObject user = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\docs\\demo.json"));
            //Console.WriteLine(user.IsValid(schema));

            Console.WriteLine(Path.GetDirectoryName("C:\\Some\\Path\\file.json") + Path.DirectorySeparatorChar + "imageFile");
            

            //Application app = new Application();
            //mainWindow = new IDTMForm();
            //app.Run(mainWindow);
            
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "idtm.docs.schema.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)){
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }

            //Outputs the imgs NO VALIDATION TILL NOW
            /*foreach(Img img in Bio.ReadFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json")){
                Console.WriteLine(img.name);
                for(int i = 0; i < img.names.Count; i++){
                    Console.WriteLine("  " + img.names[i]);
                    Console.WriteLine("    " + img.values[i]);
                }
            }*/

    	    //Read and Save test
            //Console.WriteLine(Bio.SaveFile(Bio.ReadFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json"), Directory.GetCurrentDirectory() + "\\docs\\test.json"));

            //Validation test
            //Console.WriteLine(Bio.Validate(Directory.GetCurrentDirectory() + "\\demo\\demo.json"));

            //Aio.OpenFile(Directory.GetCurrentDirectory() + "\\demo\\demo.json");

            /* NEW

            Bio.Open(Directory.GetCurrentDirectory() + "\\docs\\exam.json");
            foreach(ITL itl in Bio.iTLs){
                Console.WriteLine("Name: {0}", itl.name);
                for(int i = 0; i < itl.values.Count; i++){
                    Console.WriteLine("  {0}: {1}", Bio.header[i], itl.values[i]);
                }
            }
            Console.WriteLine("Path: {0}; File: {1}", Bio.dir, Bio.file);

            foreach(string filese in Bio.GetFiles(Directory.GetCurrentDirectory() + "\\demo")){
                Console.WriteLine(filese);
            }
            
            */
            




            Console.ReadKey();
        }
    }

    

}
