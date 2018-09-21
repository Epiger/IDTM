using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using Idtm.IO;
using System.Collections.Generic;

namespace Idtm {

    public class IDTMForm : Form {
	    public IDTMForm(){
		    // sets the client (inner) size of the window for your content
		    this.ClientSize = new Size(600, 400);

		    this.Title = "IDTM";

            ToolBar = new ToolBar{
                Items ={ 
                    new CreateCommand(),
                    new OpenCommand(),
                    new SeparatorToolItem(),
                    new SaveCommand()
                }
            };

        
	    }

        
    }

    class CreateCommand : Command {
        

        public CreateCommand(){
            //Text
		    ToolBarText = "Create";
            ToolTip = "Creates a new project";
            //Icon
            Image = Bitmap.FromResource("idtm.icons.Create_16x.png");
            //Shortcut
            Shortcut = Application.Instance.CommonModifier | Keys.O;
        }

        protected override void OnExecuted(EventArgs e){
		    base.OnExecuted(e);
            //When the user taps on Create

            //Open a folder
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Open Folder";
            dialog.FileName = "Idtmf";
            dialog.Filters.Add(new FileFilter("IDTM file (*.json)", new string[]{".json"}));
            dialog.ShowDialog(Program.mainWindow);

            

            if(dialog.FileName.Contains(Path.DirectorySeparatorChar.ToString())){
                Program.actualFile = dialog.FileName;
                Bio.CreateFile(dialog.FileName);

                //LATER AUTOMATICLY OPEN FILE                
            }
            return;
        }
    }

    class OpenCommand : Command {
        

        public OpenCommand(){
            //Text
		    ToolBarText = "Open";
            ToolTip = "Open's a Folder";
            //Icon
            Image = Bitmap.FromResource("idtm.icons.OpenFolder_16x.png");
            //Shortcut
            Shortcut = Application.Instance.CommonModifier | Keys.O;
        }

        protected override void OnExecuted(EventArgs e){
		    base.OnExecuted(e);
            //When the user taps on Open

            //Open a folder
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open file";
            dialog.MultiSelect = false;
            dialog.ShowDialog(Program.mainWindow);
            
            if(dialog.FileName != null){
                //No dialog interrupt
                if(File.Exists(dialog.FileName)){
                    //Does the File Exist
                    if(Bio.Validate(dialog.FileName)){
                        //Does the file has a vaild schema
                        //Set the List and the filepath string
                        Program.imgs = Bio.ReadFile(dialog.FileName);
                        Program.actualFile = dialog.FileName;
                    }else {
                        MessageBox.Show("This file doesn't have the right schema!");
                    }
                }
            }

            return;
        }
    }

    class SaveCommand : Command {
        

        public SaveCommand(){
            //Text
		    ToolBarText = "Save";
            ToolTip = "Saves the work";
            //Icon
            Image = Bitmap.FromResource("idtm.icons.Save_16x.png");
            //Shortcut
            Shortcut = Application.Instance.CommonModifier | Keys.S;
        }

        protected override void OnExecuted(EventArgs e){
		    base.OnExecuted(e);
            if(Program.actualFile != "" && File.Exists(Program.actualFile)){
                Bio.SaveFile(Program.imgs, Program.actualFile);
            }
        }
    }

}