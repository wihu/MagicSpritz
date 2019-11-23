using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace MagicSpritz
{
    public class Store<T>  where T : new()
    {
        private T _state;
        private BehaviorSubject<T> _stateSubject;
        private readonly List<Modifier<T>> _modifiers;

        public Store() : this(new T())
        {

        }

        public Store(T state)
        {
            _state = state;
            _stateSubject = new BehaviorSubject<T>(_state);
            _modifiers = new List<Modifier<T>>();
        }

        public void AddModifiers(params Modifier<T>[] modifiers)
        {
            _modifiers.AddRange(modifiers);
        }

        public void Update(IAction action)
        {
            _state = Modify(_state, action);
            _stateSubject.OnNext(_state);
        }

        private T Modify(T state, IAction action)
        {
            var modifiers = _modifiers.Where(x => x.Type == action.GetType());
            foreach (var modifier in modifiers)
            {
                state = modifier.Modify(state, action);
            }
            return state;
        }

        public IObservable<T> Select()
        {
            return _stateSubject;
        }

        public IObservable<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
    }
}
