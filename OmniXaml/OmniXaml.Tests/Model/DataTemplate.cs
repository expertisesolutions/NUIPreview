﻿namespace OmniXaml.Tests.Model
{
    using Attributes;

    [Namescope]
    public class DataTemplate
    {
        [Content]
        [FragmentLoader(FragmentLoader = typeof(ConstructionFragmentLoader))]
        public TemplateContent Content { get; set; }

        protected bool Equals(DataTemplate other)
        {
            return Equals(Content, other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((DataTemplate) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }   
}