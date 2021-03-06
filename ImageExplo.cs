using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using System.Collections.Generic;
using Idtm.IO;

namespace Idtm.Wind {

    public class ImageScroller : Scrollable {
        
        StackLayout layout;
        List<ImageItem> imageItems = new List<ImageItem>();

        public ImageScroller(){
            layout = new StackLayout(){
                
                //Items = {
                    /*new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG")),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG"))*/
                //},
                
                Orientation = Orientation.Vertical,
                HorizontalContentAlignment = HorizontalAlignment.Center

            };
            Content = layout;
            Border = BorderType.None;
            BackgroundColor = Color.FromGrayscale(0x1C);
            Size = new Size(150, -1);          

        }

        /*public void ReDraw(){
            layout.Items.Clear();
            foreach(string imgstr in Bio.filesInFolder){
                layout.Items.Add(new StackLayoutItem(new ImageItem(Bio.idtmFolder + Path.DirectorySeparatorChar + imgstr), false));
            
            
            }
            layout.Items.Add(null);

        }*/

    }

    public class ImageItem : Panel {

        //string image;
        Image bitmap;
        TableLayout layout;
        int state = 1;

        public const int inFolder = 0;
        public const int inFolderApplied = 1;
        public const int inFolderFullApplied = 2;

        public ImageItem (string image){
            bitmap = new Bitmap(image);
            layout = new TableLayout();
            //layout.BackgroundColor = Color.FromArgb(255,255,0);
            layout.Rows.Add(new TableRow(new TableCell(new ImageVii(this, bitmap), false)));
            layout.Rows.Add(new TableRow(new TableCell(new ImageLabel(this, Path.GetFileName(image)), false)));
            layout.Spacing = new Size(0, 3);
            
            
            
            
            

            Content = layout;
            //Size = new Size(115, 200);
            Padding = new Padding(5);
            SetState();
        }

        public void SetState(){SetState(state);}
        public void SetState(int state){
            //States 
            switch(state){
                case inFolder:
                    BackgroundColor = Color.FromArgb(255, 0, 0);
                    break;
                case inFolderApplied:
                    BackgroundColor = Color.FromArgb(0, 255, 0);
                    break;
                case inFolderFullApplied:
                    BackgroundColor = Color.FromArgb(0, 0, 255);
                    break;
            }
        }

        class ImageLabel : Label{
            
            ImageItem imgItem;

            public ImageLabel(ImageItem parent, string text = ""){
                imgItem = parent;
                Text = text;
            }

            protected override void OnMouseEnter(MouseEventArgs e){
                base.OnMouseEnter(e);
                imgItem.OnMouseEnter(e);
            }

            protected override void OnMouseLeave(MouseEventArgs e){
                base.OnMouseLeave(e);
                imgItem.OnMouseLeave(e);
            }

        }

        class ImageVii : ImageView {
            
            ImageItem imgItem;

            public ImageVii(ImageItem parent, Image image){
                imgItem = parent;
                Image = image;
                Size = new Size(125, (int)(125/((float)image.Width/(float)image.Height)));
            }



            protected override void OnMouseEnter(MouseEventArgs e){
                base.OnMouseEnter(e);
                imgItem.OnMouseEnter(e);
            }

            protected override void OnMouseLeave(MouseEventArgs e){
                base.OnMouseLeave(e);
                imgItem.OnMouseLeave(e);
            }

        }


        protected override void OnMouseDown(MouseEventArgs e){
            base.OnMouseDown(e);
            Console.WriteLine("YEAH");
        }
        protected override void OnMouseEnter(MouseEventArgs e){
            base.OnMouseEnter(e);
            BackgroundColor = Color.FromArgb(255,0,0);
        }

        protected override void OnMouseLeave(MouseEventArgs e){
            base.OnMouseLeave(e);
            BackgroundColor = Color.FromArgb(0,255,0);
        }

    }

    /*public class TagEditer : Scrollable {

        TableLayout layout;

        public TagEditer() {
            layout = new TableLayout(){
                Rows = {
                    new TableRow(
                        /*new TagLabel("tag1"),
                        new TagLabel("tag2")
                        new TagLabel("tag3"),
                        new TagLabel("tag4"),
                        new TagLabel("tag5"),
                        new TagLabel("tag6"),
                        new TagLabel("tag7"),
                        null*//*
                    ),
                    new TableRow(
                        /*new TagEdit(),
                        new TagEdit()
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        null*//*
                    )
                },
                Padding = new Padding(2, 5),
                Spacing = new Size(2, 5)
            };
            Content = layout;
            Size = new Size(-1, 72);
            Border = BorderType.None;
            BackgroundColor = Color.FromGrayscale(0x1C);
        }

        public void ReDraw(){
            layout = new TableLayout(){
                Rows = {new TableRow(), new TableRow()},
                Padding = new Padding(2, 5),
                Spacing = new Size(2, 5)
            };
            int i = 0;
            foreach(string tag in Bio.tagHeader){
                try{
                    if(tag == Bio.imgs[IDTMForm.imageFile].names[i]){
                        layout.Rows[0].Cells.Add(new TagLabel(Bio.imgs[IDTMForm.imageFile].names[i], 1));
                        layout.Rows[1].Cells.Add(new TagEdit(Bio.imgs[IDTMForm.imageFile].values[i].ToString()));
                        i++;
                    }else {
                        layout.Rows[0].Cells.Add(new TagLabel(tag, 0));
                        layout.Rows[1].Cells.Add(new TagEdit());
                    }
                }catch(ArgumentOutOfRangeException){
                    layout.Rows[0].Cells.Add(new TagLabel(tag, 0));
                    layout.Rows[1].Cells.Add(new TagEdit());
                }
            }
            layout.Rows[0].Cells.Add(null);
            layout.Rows[1].Cells.Add(null);
            Content = layout;
            
        }

        private class TagLabel : TableCell {

            Label mLabel;

            public const int GrayState = 0;
            public const int BlackState = 1;
            

            public TagLabel(string text = "", int state = 0){
                mLabel = new Label(){Text = text};
                SetState(state);
                this.Control = mLabel;
                this.ScaleWidth = false;        
            }

            public void SetState(int state){
                switch(state){
                    case GrayState:
                        mLabel.TextColor = Color.FromGrayscale(0xaa);
                        break;
                    case BlackState:
                        mLabel.TextColor = Color.FromGrayscale(0xFF);
                        break;
                }
            }

        }

        private class TagEdit : TableCell {

            TextBox mBox;

            public TagEdit(string text = ""){
                mBox = new TextBox(){Text = text};
                this.Control = mBox;
                this.ScaleWidth = false;
                    
            }

            public string getValue(){
                return mBox.Text;
            }

        }
            
            
}*/
    


}