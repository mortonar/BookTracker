﻿using BookTracker.HelperClasses;
using BookTracker.Messaging.Requests;
using BookTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace BookTracker.ViewModels
{
    public class ToReadListViewModel : ObservableObject, IPageViewModel
    {

        public void Update()
        {
            Debug.WriteLine("Getting the To Read Book List.");

            BookList bookList = new BookList
            {
                Name = Name
            };

            Base message = new Base
            {
                Action = "BookList",
                Payload = bookList
            };
            try
            {
                string response = ServerProxy.Instance.sendRequest(message);
                Debug.WriteLine("RESPONSE: " + response + "\n");
                Messaging.Responses.Base responseMsg = JsonConvert.DeserializeObject<Messaging.Responses.Base>(response);
                Debug.WriteLine("PAYLOD: " + responseMsg.Payload);
                Messaging.Responses.BookSearch searchResponse = (responseMsg.Payload as JObject).ToObject<Messaging.Responses.BookSearch>();
                _bookListModel = new BookListModel(searchResponse.Books);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
        }

        public string Name
        {
            get { return "To Read"; }
        }
        
        private BookListModel _bookListModel;
        public BookListModel BookListModel
        {
            get { return _bookListModel; }
            set { _bookListModel = value; }
        }
    }
}
