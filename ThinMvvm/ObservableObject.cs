// Copyright (c) Solal Pirelli 2014
// See License.txt file for more details

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace ThinMvvm
{
    /// <summary>
    /// Base class for observable objects.
    /// </summary>
    [DataContract]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        // Used to send PropertyChanged messages on the right thread
        private readonly SynchronizationContext _context;


        /// <summary>
        /// Creates a new ObservableObject.
        /// </summary>
        protected ObservableObject()
        {
            _context = SynchronizationContext.Current;
        }


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Fires a property changed event.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            var evt = this.PropertyChanged;
            if ( evt != null )
            {
                if ( _context == null )
                {
                    evt( this, new PropertyChangedEventArgs( propertyName ) );
                }
                else
                {
                    _context.Send( _ => evt( this, new PropertyChangedEventArgs( propertyName ) ), null );
                }
            }
        }

        /// <summary>
        /// Sets the specified field to the specified value and raises <see cref="PropertyChanged"/>.
        /// </summary>
        protected void SetProperty<T>( ref T field, T value, [CallerMemberName] string propertyName = "" )
        {
            if ( !object.Equals( field, value ) )
            {
                field = value;
                this.OnPropertyChanged( propertyName );
            }
        }
    }
}