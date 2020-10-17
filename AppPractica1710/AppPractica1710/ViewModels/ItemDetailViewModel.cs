using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AppPractica1710.Models;
using AppPractica1710.Services;
using Xamarin.Forms;

namespace AppPractica1710.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }
        bool isNew;
        public bool IsNew
        {
            get => isNew;
            set => SetProperty(ref isNew, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }


        public ToDoItem ToDoItem { get; set; }
        public ICommand SaveCommand { get; }

        public event EventHandler SaveComplete;

        public ItemDetailViewModel(ToDoItem todo, bool isNew)
        {
            IsNew = isNew;
            ToDoItem = todo;

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = IsNew ? "New To Do" : ToDoItem.Name;
        }

        async Task ExecuteSaveCommand()
        {
            if (IsNew)
                await CosmosDBService.InsertToDoItem(ToDoItem);
            else
                await CosmosDBService.UpdateToDoItem(ToDoItem);

            SaveComplete?.Invoke(this, new EventArgs());
        }
    }
}
