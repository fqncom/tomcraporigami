using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Entity;
using TickTick.Models;
using TickTick.Synchronous;

namespace TickTick.ViewModels
{
    public class SignInPageViewModel
    {

        public Communicator Communicator { get; set; }

        public User SignUserInfo { get; set; }

        public SignInPageViewModel()
        {
            Communicator = new Communicator();
            //SignUserInfo = new SignUserInfo();
        }
    }
}
