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
                    new OpenCommand(),
                    new SeparatorToolItem(),
                    new SaveCommand()
                }
            };

            //FilePick pick = new FilePick();
            //pick.ShowDialog(this);
            //MessageBox.Show(Application.Instance.MainForm, "You clicked me!", "Tutorial 2", MessageBoxButtons.OK);
	    }

        
    }

    class OpenCommand : Command {
        

        public OpenCommand(){
            MenuText = "Open";
		    ToolBarText = "Open";
            ToolTip = "Open's a Folder";
            //Image = Icon.FromResource ("MyResourceName.ico");
            
            //Image = Bitmap.FromResource("OpenFolder_16x.png");
            Image = new Bitmap("icons\\OpenFolder_16x.png");
            //Bitmap.FromResource("idmt.icons.OpenFolder_16x.png");
            Shortcut = Application.Instance.CommonModifier | Keys.M;  // control+M or cmd+M
        }

        protected override void OnExecuted(EventArgs e){
		    base.OnExecuted(e);

		    MessageBox.Show(Application.Instance.MainForm, "You clicked me!", "Tutorial 2", MessageBoxButtons.OK);
        }
    }

    class SaveCommand : Command {
        

        public SaveCommand(){
            MenuText = "Save";
		    ToolBarText = "Save";
            ToolTip = "Saves the work";
            //Image = Icon.FromResource ("MyResourceName.ico");
            //Image = Bitmap.FromResource ("Idtm.icons.Save_16x.png");
            Shortcut = Application.Instance.CommonModifier | Keys.S;  // control+M or cmd+M
        }

        protected override void OnExecuted(EventArgs e){
		    base.OnExecuted(e);

		    MessageBox.Show(Application.Instance.MainForm, "You clicked me!", "Tutorial 2", MessageBoxButtons.OK);
        }
    }

    class FilePick : FileDialog {

        public FilePick(){
            this.Title = "Open";
        }

    }

}