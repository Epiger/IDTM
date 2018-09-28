using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;

namespace Idtm.Wind {

    public class ImageScroller : Scrollable {


        public ImageScroller(){
            Content = new StackLayout(){
                Items = {
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageView(){
                        Image = new Bitmap("IMG_0067.JPG"),
                        Size = new Size(100, 75)   
                    }),
                    new StackLayoutItem(new ImageItem("IMG_0067.JPG"))
                },
                Orientation = Orientation.Vertical,
                HorizontalContentAlignment = HorizontalAlignment.Center

            };
            Border = BorderType.None;
            BackgroundColor = Color.FromGrayscale(0x1C);
            Size = new Size(150, -1);          

        }

    }

    public class ImageItem : Panel {

        string image;
        Image bitmap;

        public ImageItem (string image){
            Console.WriteLine(Path.GetFullPath("D:\\Fme\\IDTM\\docs\\demo.json") + image);
            bitmap = new Bitmap(image);
            Content = new ImageVii(this, bitmap);
            Size = new Size(115, 75);
            Padding = new Padding(5);
            BackgroundColor = Color.FromArgb(0, 255, 0);
            


        }

        class ImageVii : ImageView {
            
            ImageItem imgItem;

            public ImageVii(ImageItem parent, Image image){
                imgItem = parent;
                Image = image;
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

        /*public void mDown(MouseEventArgs e){
            OnMouseDown(e);
        }
        public void mEnter(MouseEventArgs e){
            OnMouseEnter(e);
        }
        public void mLeave(MouseEventArgs e){
            OnMouseLeave(e);
        }*/


    }

    public class TagEditer : Scrollable {

        public TagEditer() {
            this.Content = new TableLayout(){
                Rows = {
                    new TableRow(
                        new TagLabel("tag1"),
                        new TagLabel("tag2"),
                        new TagLabel("tag3"),
                        new TagLabel("tag4"),
                        new TagLabel("tag5"),
                        new TagLabel("tag6"),
                        new TagLabel("tag7"),
                        null
                    ),
                    new TableRow(
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        new TagEdit(),
                        null
                    )
                },
                Padding = new Padding(2, 5),
                Spacing = new Size(2, 5)
            };
            Size = new Size(-1, 72);
            
            
        }

        class TagLabel : TableCell {

            Label mLabel;

            public TagLabel(string text = ""){
                mLabel = new Label(){Text = text};
                this.Control = mLabel;
                this.ScaleWidth = false;
                
            }

        }

        class TagEdit : TableCell {
            
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

    }
    


}