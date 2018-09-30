using System;
using System.IO;
using Idtm;

namespace Idtm.IO{

    public class Aio {

        public static void OpenFile(string file){
            //Reads the files and sets the vars
            Bio.imgs = Bio.ReadFile(file);
            Bio.idtmFile = file;
            Bio.idtmFolder = Path.GetDirectoryName(file);

            //Addes the files inside of the specifed folder
            foreach(string fileIF in Directory.GetFiles(Bio.idtmFolder)){
                if(MatchesExt(fileIF)){
                    Bio.filesInFolder.Add(Path.GetFileName(fileIF));
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
            switch (e.ChangeType){
                case WatcherChangeTypes.Created:
                    Console.WriteLine("Created: " + e.Name);
                    if(MatchesExt(e.Name)){
                        AddIfNecessary(e.Name);
                    }
                    //Redraw
                    Program.mainWindow.ReDraw();
                    break;
                case WatcherChangeTypes.Deleted:
                    Console.WriteLine("Deleted: " + e.Name);
                    //Remove it from the folder record
                    Bio.filesInFolder.Remove(e.Name);
                    int index = Img.IndexOf(e.Name, Bio.imgs);
                    if(index != -1){
                        //If it exits in the imgs remove it
                        Bio.imgs.RemoveAt(index);
                    }
                    //Redraw
                    Program.mainWindow.ReDraw();        
                    break;
            }
            foreach(Img img in Bio.imgs){
                Console.WriteLine(img.name);
            }
            Console.WriteLine();
            foreach(string file in Bio.filesInFolder){
                Console.WriteLine(file);
            }
        }

        private static void OnRenamed(object source, RenamedEventArgs e){
            Console.WriteLine("Renamed: " + e.Name + " " + e.OldName);
            if(e.ChangeType == WatcherChangeTypes.Renamed && MatchesExt(e.OldName) && MatchesExt(e.Name) && Bio.filesInFolder.Contains(e.OldName)){
                //Update filesInFolders
                Bio.filesInFolder[Bio.filesInFolder.IndexOf(e.OldName)] = e.Name;
                //Update imgs
                int index = Img.IndexOf(e.OldName, Bio.imgs);
                if(index != -1){
                    //If he found a match in the collected files he renames it
                    Bio.imgs[index].name = e.Name;
                }
                //Redraw
                Program.mainWindow.ReDraw();
            }else if(e.ChangeType == WatcherChangeTypes.Renamed && MatchesExt(e.OldName) && !MatchesExt(e.Name) && Bio.filesInFolder.Contains(e.OldName)){
                //Remove it from the folder record
                Bio.filesInFolder.Remove(e.Name);
                //Redraw
                Program.mainWindow.ReDraw();             
            }else if(e.ChangeType == WatcherChangeTypes.Renamed && MatchesExt(e.Name)){
                //Add to filesInFolders if necessary
                AddIfNecessary(e.Name);
                //Redraw
                Program.mainWindow.ReDraw();
            }
            foreach(Img img in Bio.imgs){
                Console.WriteLine(img.name);
            }
            Console.WriteLine();
            foreach(string file in Bio.filesInFolder){
                Console.WriteLine(file);
            }
        }

        public static void AddIfNecessary(string name){
            //Check if it doesn't exist in filesInFolder
            if(!Bio.filesInFolder.Contains(name)){
                //Add to Bio.filesInFolder
                Bio.filesInFolder.Add(name);
            }
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

}