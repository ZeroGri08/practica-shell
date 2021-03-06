﻿using AppPractica1710.Models;
using AppPractica1710.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPractica1710.ViewModels
{
    public class CompletedItemsViewModel : BaseViewModel
    {
		List<ToDoItem> completedToDos;
		public List<ToDoItem> CompletedToDos { get => completedToDos; set => SetProperty(ref completedToDos, value); }

		public ICommand RefreshCommand { get; }

		public CompletedItemsViewModel()
		{
			RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
			Title = "Completed";
		}

		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				CompletedToDos = await CosmosDBService.GetCompletedToDoItems();
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
