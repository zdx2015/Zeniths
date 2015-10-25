using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zeniths.WorkFlow.Utility
{
    public class WorkFlowDesign
    {
        [JsonProperty("property")]
        public FlowProperty Property { get; set; }

        [JsonProperty("flow")]
        public FlowDesign Flow { get; set; }

        [JsonProperty("step")]
        public Dictionary<string, FlowStepSetting> Step { get; set; } = new Dictionary<string, FlowStepSetting>();

        [JsonProperty("line")]
        public Dictionary<string, FlowLineSetting> Line { get; set; } = new Dictionary<string, FlowLineSetting>();

    }

    public class FlowProperty
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("isDebug")]
        public bool IsDebug { get; set; }

        [JsonProperty("debugUserIds")]
        public string DebugUserIds { get; set; }

        [JsonProperty("sortIndex")]
        public int SortIndex { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }

    public class FlowDesign
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("nodes")]
        public Dictionary<string, FlowStep> Steps { get; set; } = new Dictionary<string, FlowStep>();

        [JsonProperty("lines")]
        public Dictionary<string, FlowLine> Lines { get; set; } = new Dictionary<string, FlowLine>();

        [JsonProperty("areas")]
        public Dictionary<string, FlowArea> Areas { get; set; } = new Dictionary<string, FlowArea>();

        [JsonProperty("initNum")]
        public int InitNum { get; set; }
    }

    public class FlowStep
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }

    public class FlowStepSetting
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("signatureType")]
        public string SignatureType { get; set; }

        [JsonProperty("warningHour")]
        public string WarningHour { get; set; }

        [JsonProperty("processHour")]
        public string ProcessHour { get; set; }

        [JsonProperty("formName")]
        public string FormName { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("flowCategory")]
        public string FlowCategory { get; set; }

        [JsonProperty("processPolicy")]
        public string ProcessPolicy { get; set; }

        [JsonProperty("processPercentage")]
        public string ProcessPercentage { get; set; }

        [JsonProperty("countersignPolicy")]
        public string CountersignPolicy { get; set; }

        [JsonProperty("countersignPercentage")]
        public string CountersignPercentage { get; set; }

        [JsonProperty("backPolicy")]
        public string BackPolicy { get; set; }

        [JsonProperty("backCategory")]
        public string BackCategory { get; set; }

        [JsonProperty("backStep")]
        public string BackStep { get; set; }

        [JsonProperty("handlerSelectPolicy")]
        public string HandlerSelectPolicy { get; set; }

        [JsonProperty("handlerCategory")]
        public string HandlerCategory { get; set; }

        [JsonProperty("handlerSelectRange")]
        public string HandlerSelectRange { get; set; }

        [JsonProperty("handlerDefault")]
        public string HandlerDefault { get; set; }

        [JsonProperty("eventSubmitBefore")]
        public string EventSubmitBefore { get; set; }

        [JsonProperty("eventSubmitAfter")]
        public string EventSubmitAfter { get; set; }

        [JsonProperty("eventBackBefore")]
        public string EventBackBefore { get; set; }

        [JsonProperty("eventBackAfter")]
        public string EventBackAfter { get; set; }

        [JsonProperty("expiredPrompt")]
        public bool ExpiredPrompt { get; set; }
    }

    public class FlowLine
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }

    public class FlowLineSetting
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("organize_senderin")]
        public string OrganizeSenderin { get; set; }

        [JsonProperty("organize_sendernotin")]
        public string OrganizeSendernotin { get; set; }

        [JsonProperty("organize_sponsorin")]
        public string OrganizeSponsorin { get; set; }

        [JsonProperty("organize_sponsornotin")]
        public string OrganizeSponsornotin { get; set; }

        [JsonProperty("organize_senderleader")]
        public bool OrganizeSenderleader { get; set; }

        [JsonProperty("organize_senderchargeleader")]
        public bool OrganizeSenderchargeleader { get; set; }

        [JsonProperty("organize_sponsorleader")]
        public bool OrganizeSponsorleader { get; set; }

        [JsonProperty("organize_sponsorchargeleader")]
        public bool OrganizeSponsorchargeleader { get; set; }

        [JsonProperty("organize_notsenderleader")]
        public bool OrganizeNotsenderleader { get; set; }

        [JsonProperty("organize_notsenderchargeleader")]
        public bool OrganizeNotsenderchargeleader { get; set; }

        [JsonProperty("organize_notsponsorleader")]
        public bool OrganizeNotsponsorleader { get; set; }

        [JsonProperty("organize_notsponsorchargeleader")]
        public bool OrganizeNotsponsorchargeleader { get; set; }
    }

    public class FlowArea
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }
}