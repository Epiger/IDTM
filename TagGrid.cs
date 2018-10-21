using System;
using Eto.Forms;
using Eto;
using System.Collections.ObjectModel;

namespace Idtm.Wind {

    class TagItem {

        public string Name{get; set;}
        public int Value{get; set;}

    }

    public class TagGrid : GridView{
        
        public TagGrid(){

            var collection = new ObservableCollection<TagItem>();
            collection.Add(new TagItem(){Name = "tag1", Value = 42});
            collection.Add(new TagItem(){Name = "tag2", Value = 43});
            
            DataStore = collection;

            Columns.Add(new GridColumn(){
                DataCell = new TextBoxCell(){Binding = Binding.Property<TagItem, string>(r => r.Name)},
                HeaderText = "Name"
            });
            Columns.Add(new GridColumn(){
                DataCell = new TextBoxCell(){Binding = Binding.Property<TagItem, string>(r => r.Value.ToString())},
                HeaderText = "Value"
            });
        }

    }

}