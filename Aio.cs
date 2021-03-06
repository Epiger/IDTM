using System;
using System.IO;
using Idtm;

namespace Idtm.IO{

    public class Aio {

        /*public static void OpenFile(string file){
            //Reads the files and sets the vars
            Bio.imgs = Bio.ReadFile(file);
            Bio.idtmFile = file;
            Bio.idtmFolder = Path.GetDirectoryName(file);

            //Addes the files to filesInFolder
            Bio.filesInFolder.Clear();
            foreach(string fileIF in Directory.GetFiles(Bio.idtmFolder)){
                if(Bio.MatchesExt(fileIF)){
                    Bio.filesInFolder.Add(Path.GetFileName(fileIF));
                }                
            }

            //addes the listener
            AddFolderWatcher(Bio.idtmFolder);

            Program.mainWindow.ReDraw();

            
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
                    //Console.WriteLine("Created: " + e.Name);
                    if(Bio.MatchesExt(e.Name)){
                        //Add to filesInFolder
                        Add(e.Name);
                    }
                    //Redraw
                    Program.mainWindow.ReDraw();
                    break;
                case WatcherChangeTypes.Deleted:
                    //Console.WriteLine("Deleted: " + e.Name);
                    //Remove from filesInFolder and imgs
                    Remove(e.Name);
                    //Redraw
                    Program.mainWindow.ReDraw();        
                    break;
            }
        }

        private static void OnRenamed(object source, RenamedEventArgs e){
            //Console.WriteLine("Renamed: " + e.Name + " " + e.OldName);
            if(e.ChangeType == WatcherChangeTypes.Renamed && Bio.MatchesExt(e.OldName) && Bio.MatchesExt(e.Name) && Bio.filesInFolder.Contains(e.OldName)){
                //If one of the images got a rename
                //Rename the entry in filesInFolder and imgs
                Rename(e.Name, e.OldName);
                //Redraw
                Program.mainWindow.ReDraw();
            }else if(e.ChangeType == WatcherChangeTypes.Renamed && Bio.MatchesExt(e.OldName) && !Bio.MatchesExt(e.Name) && Bio.filesInFolder.Contains(e.OldName)){
                //If the new name isn't an image file
                //Remove it from the folder record
                Bio.filesInFolder.Remove(e.Name);
                //Redraw
                Program.mainWindow.ReDraw();             
            }else if(e.ChangeType == WatcherChangeTypes.Renamed && Bio.MatchesExt(e.Name)){
                //If an image file was created
                //Add to filesInFolders
                Add(e.Name);
                //Redraw
                Program.mainWindow.ReDraw();
            }
        }
        


        public static void Add(string name){
            //Check if it doesn't exist in filesInFolder
            if(!Bio.filesInFolder.Contains(name)){
                //Add to Bio.filesInFolder
                Bio.filesInFolder.Add(name);
            }
        }
        public static void Remove(string name){
            //Remove form filesInFolder
            Bio.filesInFolder.Remove(name);
            //Remove from imgs
            int index = Img.IndexOf(name, Bio.imgs);
            if (index > -1 && index < Bio.imgs.Count){
                Bio.imgs.RemoveAt(index);
            }
        }
        public static void Rename(string name, string oldName){
            //Update filesInFolders
            Bio.filesInFolder[Bio.filesInFolder.IndexOf(oldName)] = name;
            //Update imgs
            int index = Img.IndexOf(oldName, Bio.imgs);
            if(index != -1 && index < Bio.imgs.Count){
                //If he found a match in the collected files he renames it
                Bio.imgs[index].name = name;
            }
        }*/

    }

}