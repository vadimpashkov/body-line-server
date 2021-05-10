using Domain.Abstractions.Outputs;

namespace Domain.Abstractions.Queries
{
    public class ObjectOutput: IOutput
    {
        public object Object { get; set; }

        public static ObjectOutput Create(object result)
        {
            return new ObjectOutput
            {
                Object = result,
            };
        }

        public static ObjectOutput CreateWithId(int id)
        {
            return Create(new
            {
                Id = id,
            });
        }
        
        private ObjectOutput() {}
    }
}