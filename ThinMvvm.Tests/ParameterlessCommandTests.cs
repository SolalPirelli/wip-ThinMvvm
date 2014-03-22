﻿// Copyright (c) Solal Pirelli 2014
// See License.txt file for more details

using System.ComponentModel;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThinMvvm.Tests
{
    [TestClass]
    public sealed class ParameterlessCommandTests
    {
        [TestMethod]
        public void ExecuteCallsTheProvidedExecuteMethod()
        {
            int count = 0;
            var cmd = new Command( null, () => count++ );

            cmd.Execute();

            Assert.AreEqual( 1, count, "Execute() should call the provided 'execute' parameter exactly once." );
        }

        [TestMethod]
        public void ICommandExecuteCallsTheProvidedExecuteMethod()
        {
            int count = 0;
            var cmd = new Command( null, () => count++ );

            ( (ICommand) cmd ).Execute( null );

            Assert.AreEqual( 1, count, "ICommand.Execute() should call the provided 'execute' parameter exactly once." );
        }

        [TestMethod]
        public void CanExecuteIsTrueWhenNotProvided()
        {
            var cmd = new Command( null, () => { } );

            Assert.AreEqual( true, cmd.CanExecute(), "CanExecute() should return true when the 'canExecute' parameter is not provided." );
        }

        [TestMethod]
        public void CanExecuteCallsTheProvidedCanExecuteMethod()
        {
            int n = 0;
            var cmd = new Command( null, () => { }, () => n == 42 );

            Assert.AreEqual( false, cmd.CanExecute(), "CanExecute() should call the provided 'canExecute' parameter." );
            n = 42;
            Assert.AreEqual( true, cmd.CanExecute(), "CanExecute() should call the provided 'canExecute' parameter." );
        }

        [TestMethod]
        public void ICommandCanExecuteCallsTheProvidedCanExecuteMethod()
        {
            int n = 0;
            var cmd = new Command( null, () => { }, () => n == 42 );

            Assert.AreEqual( false, ( (ICommand) cmd ).CanExecute( null ), "ICommand.CanExecute() should call the provided 'canExecute' parameter." );
            n = 42;
            Assert.AreEqual( true, ( (ICommand) cmd ).CanExecute( null ), "ICommand.CanExecute() should call the provided 'canExecute' parameter." );
        }

        private sealed class InpcExample : INotifyPropertyChanged
        {
            public int Value { get; set; }

            public void TestAsyncCommand()
            {
                var cmd = new Command( null, () => { }, () => Value == 0 );
                int count = 0;

                cmd.CanExecuteChanged += ( s, e ) => count++;
                OnPropertyChanged( "Value" );

                Assert.AreEqual( 1, count, "CanExecuteChanged should be fired exactly once when a property it uses changes." );
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged( string propertyName )
            {
                var evt = PropertyChanged;
                if ( evt != null )
                {
                    evt( this, new PropertyChangedEventArgs( propertyName ) );
                }
            }
        }

        [TestMethod]
        public void CanExecuteChangedShouldBeFiredWhenAPropertyChanges()
        {
            new InpcExample().TestAsyncCommand();
        }

        [TestMethod]
        public void CanExecuteChangedShouldBeFiredWhenAPropertyOfAFieldChanges()
        {
            var ex = new InpcExample();
            var cmd = new Command( null, () => { }, () => ex.Value == 1 );
            int count = 0;

            cmd.CanExecuteChanged += ( s, e ) => count++;
            ex.OnPropertyChanged( "Value" );

            Assert.AreEqual( 1, count, "CanExecuteChanged should be fired exactly once when a property it uses changes, even in a closure." );
        }
    }
}