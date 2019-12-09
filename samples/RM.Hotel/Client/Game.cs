using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MagicSpritz;
using McMaster.Extensions.CommandLineUtils;

namespace RM.Hotel
{
    public class Game : CommandLineApplication
    {
        private IStoreUpdater _store;

        public Game(IStoreUpdater store)
        {
            _store = store;
        }

        public void AddAsyncCommand(string arg0, string arg1, Func<Task> action)
        {
            var main = GetOrCreateCommand(arg0);
            main.Command(arg1, sub => 
            {
                sub.OnExecuteAsync(async ct => await action.Invoke());
            });
        }

        public void AddCommand(string arg0, string arg1, Action action)
        {
            var main = GetOrCreateCommand(arg0);
            main.Command(arg1, sub => 
            {
                sub.OnExecute(() => action.Invoke());
            });
        }

        public void AddCommand<T2>(string arg0, string arg1, Func<T2, IAction> CreateStoreAction)
        where T2 : IConvertible
        {
            var main = GetOrCreateCommand(arg0);
            main.Command(arg1, sub => 
            {
                var arg2 = sub.Argument("arg2", "arg2");
                
                sub.OnExecute(() => 
                {
                    if (arg2.Value == null)
                    {
                        Console.WriteLine($"{arg0} {arg1} <{arg2.Name}>");
                        return;
                    }
                    var val2 = (T2)Convert.ChangeType(arg2.Value, typeof(T2));
                    var action = CreateStoreAction(val2);
                    _store.UpdateStore(action);
                });
            });
        }

        public void AddCommand<T2, T3>(string arg0, string arg1, Func<T2, T3, IAction> CreateStoreAction)
        where T2 : IConvertible
        where T3 : IConvertible
        {
            var main = GetOrCreateCommand(arg0);
            main.Command(arg1, sub => 
            {
                var arg2 = sub.Argument("arg2", "arg2");
                var arg3 = sub.Argument("arg3", "arg3");

                sub.OnExecute(() => 
                {
                    if (arg2.Value == null || arg3.Value == null)
                    {
                        Console.WriteLine($"{arg0} {arg1} <{arg2.Name}> <{arg3.Name}>");
                        return;
                    }
                    var val2 = (T2)Convert.ChangeType(arg2.Value, typeof(T2));
                    var val3 = (T3)Convert.ChangeType(arg3.Value, typeof(T3));
                    var action = CreateStoreAction(val2, val3);
                    _store.UpdateStore(action);
                });
            });
        }

        private CommandLineApplication GetOrCreateCommand(string name)
        {
            var command = Commands.Find(x => x.Name == name);
            if (command == null)
            {
                command = Command(name, null);
            }
            return command;
        }
    }
}
