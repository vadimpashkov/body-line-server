﻿namespace Domain.Abstractions.Outputs
{
    public class ActionOutput: IOutput
    {
        public bool Succeeded { get; }

        public object Data { get; }
        public string ErrorMessage { get; }

        public static ActionOutput Success => new ActionOutput(true);
        public static ActionOutput SuccessData(object data) => new ActionOutput(true, null, data);

        public static ActionOutput Failure(string errorMessage)
            => new ActionOutput(false, errorMessage);

        private ActionOutput(bool succeeded, string errorMessage = null, object data = null)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}