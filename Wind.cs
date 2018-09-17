using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;

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

            SelectFolderDialog dialog = new SelectFolderDialog();
            dialog.Title = "Open Folder";
            dialog.ShowDialog(Program.mainWindow);
            Console.WriteLine(dialog.Directory);

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

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open file";
            dialog.MultiSelect = false;
            Console.WriteLine(dialog.FileName);

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

		    MessageBox.Show(Application.Instance.MainForm, "You clicked me!", "Tutorial 2", MessageBoxButtons.OK);
        }
    }

}