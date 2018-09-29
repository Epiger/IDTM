using System;
using System.IO;

namespace Idtm.IO{

    public class Aio {

        public static void OpenFile(string file){
            Bio.imgs = Bio.ReadFile(file);
            Bio.idtmFile = file;
            Bio.idtmFolder = Path.GetDirectoryName(file);
            //Addes the files inside of the specifed folder
            foreach(string fileIF in Directory.GetFiles(Bio.idtmFolder)){
                foreach(string ext in Bio.formats){
                    if(Path.GetFileName(fileIF).Equals(ext, StringComparison.InvariantCultureIgnoreCase)){
                        Bio.filesInFolder.Add(Path.GetFileName(fileIF));
                    }
                }
                
            }
            //addes the listener
            AddFolderWatcher(Bio.idtmFolder);
        }


        public static void AddFolderWatcher(string folder){
            //Init with setting of the folder
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = folder;
            //what should be watched for
            watcher.Created += new FileSystemEventHandler(OnChanged);    
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //Enable the listening
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object source, FileSystemEventArgs e){
            
        }
        private static void OnRenamed(object source, RenamedEventArgs e){

        }

    }

}