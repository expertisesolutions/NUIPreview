namespace OmniXaml.Services
{
    public class MarkupExtensionValuePipeline : ValuePipeline
    {
        public MarkupExtensionValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable, INodeToObjectBuilder builder, BuilderContext context)
        {
            var extension = mutable.Value as IMarkupExtension;
            if (extension != null)
            {
                var keyedInstance = new KeyedInstance(parent);
                var assignment = new Assignment(keyedInstance, member, mutable.Value);
                var finalValue = extension.GetValue(new ExtensionValueContext(assignment, null, null, null));
                mutable.Value = finalValue;
            }
        }
    }
}