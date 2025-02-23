
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace ArtifactKnowledge;

public class Localization
{
    public static void DialogLinesPatching()
    {
        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                "askVerrenIdentify_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Help me identify something?"},
                    {ModLanguage.Chinese, "帮我鉴定个东西？"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_showItem",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "What is it? Let me take a look."},
                    {ModLanguage.Chinese, "什么东西？拿出来让我瞧瞧？"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_showItem_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "It's this stuff."},
                    {ModLanguage.Chinese, "是这个东西。"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_showItem_abort",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Never mind, next time."},
                    {ModLanguage.Chinese, "算了，下次吧。"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_checkingItem",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Let's see... This stuff... I think I've seen it somewhere before. Let me look again..."},
                    {ModLanguage.Chinese, "我想想...这个东西...似乎在哪见过？我再看看..."}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_identifyItem",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Ha! So it's this stuff! I've seen something similar before. Come on, come on, let me tell you what this thing is..."},
                    {ModLanguage.Chinese, "哈！原来是这个玩意儿！我以前见过类似的东西。来来来，我给你讲讲这东西是什么..."}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_thankVerren_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Thank you so much Verren! You are so knowledgeable!"},
                    {ModLanguage.Chinese, "太感谢你了维伦！你真是见多识广啊！"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_failure",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I've been looking at this for a while and I still can't figure this stuff out. I'm so sorry."},
                    {ModLanguage.Chinese, "我看了半天，还是搞不明白这玩意儿。真是抱歉。"}
                }
            ),
            new LocalizationSentence(
                "askVerrenIdentify_goodbye_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "All right... But I have something else."},
                    {ModLanguage.Chinese, "好吧... 但我还有其他事。"}
                }
            )
        );
    }

    public static void ActionLogsPatching()
    {
        List<string> logList = new List<string>();
        string id = "artifactKnowledgeImprove";
        string text_en = @"~w~$~/~'s ability to identify items has improved to ~lg~$%~/~!";
        string text_zh = @"~w~$~/~的鉴定能力提升到了~lg~$%~/~！";
        logList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "verrenIdentifiedItem";
        text_en = @"~w~$~/~ identified $ for you.";
        text_zh = @"~w~$~/~帮你鉴定了$。";
        logList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));


        string logtextend = ";" + string.Concat(Enumerable.Repeat("text_end;", 12));

        List<string> log_table = ModLoader.GetTable("gml_GlobalScript_table_log");
        log_table.InsertRange(log_table.IndexOf(logtextend), logList);
        ModLoader.SetTable(log_table, "gml_GlobalScript_table_log");
    }
}