using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navigation.UI.Extensions
{
    public static class ComponentExtension
    {
        public static TComponent WithProperty<TComponent>(this TComponent component, string propertyName, Object value)
            where TComponent : Component
        {
            component.GetType().GetProperty(propertyName).SetValue(component, value);
            
            return component;
        }

        public static TComponent WithItems<TComponent>(this TComponent component, string propertyName, params Object[] elements)
            where TComponent : Component
        {
            var collection = component.GetType().GetProperty(propertyName).GetValue(component);
            
            foreach (var element in elements)
            {
                collection.GetType().GetMethod("Add", new [] {element.GetType()}).Invoke(collection, new [] {element});
            }

            return component;
        }
        
        public static TComponent WithEventHandler<TComponent>(this TComponent component, string eventName, EventHandler handler)
            where TComponent : Component
        {
            component.GetType().GetEvent(eventName).AddEventHandler(component, handler);

            return component;
        }

        public static TComponent WithMouseEventHandler<TComponent>(this TComponent component, string eventName, MouseEventHandler handler)
            where TComponent : Component
        {
            component.GetType().GetEvent(eventName).AddEventHandler(component, handler);

            return component;
        }
    }
}
