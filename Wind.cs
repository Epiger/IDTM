using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using Idtm.IO;
using System.Collections.Generic;

namespace Idtm.Wind {

    public class IDTMForm : Form {

        public static string imageFile = "";

        private Label titleLabel;
        private ImageView mainImage;
        private TagEditer tagEditer;
        private ImageScroller imageExplor;

	    public IDTMForm(){
		    // sets the client (inner) size of the window for your content
		    this.ClientSize = new Size(600, 400);

		    this.Title = "IDTM";

            //Toolbar
            ToolBar = new ToolBar{
                Items ={ 
                    new CreateCommand(),
                    new OpenCommand(),
                    new SeparatorToolItem(),
                    new SaveCommand()
                }
            };
            
            //Layout
            DynamicLayout layout = new DynamicLayout();


            titleLabel = new Label();
            mainImage = new ImageView();
            tagEditer = new TagEditer();
            imageExplor = new ImageScroller();
            

            layout.BeginHorizontal();
            layout.BeginVertical(new Padding(5), new Size(5,5), true, false);
            layout.Add(titleLabel, false, false);
            layout.Add(mainImage, true, true);
            layout.Add(tagEditer, true, false);
            layout.EndVertical();
            layout.BeginVertical(new Padding(5), new Size(5,5), false, true);
            layout.Add(imageExplor);
            layout.EndVertical();
            layout.EndHorizontal();

            

            Content = layout;          

        
	    }

        public void ReDraw(){
            /*titleLabel.Text = imageFile;
            mainImage.Image = new Bitmap(Path.GetFullPath(Bio.idtmFile) + Path.DirectorySeparatorChar + imageFile);*/
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
            Shortcut = Application.Instance.CommonModifier | Keys.N;
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
                Bio.idtmFile = dialog.FileName;
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
                        Aio.OpenFile(dialog.FileName);
                        
                        
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
            if(Bio.idtmFile != "" && File.Exists(Bio.idtmFile)){
                Bio.SaveFile(Bio.imgs, Bio.idtmFile);
            }
        }
    }

}