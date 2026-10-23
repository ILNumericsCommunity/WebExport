using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies marker shape options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Symbol
{
    /// <summary>Draws the circle marker symbol.</summary>
    [JsonStringEnumMemberName("circle")]
    Circle,

    /// <summary>Draws the circle open marker symbol.</summary>
    [JsonStringEnumMemberName("circle-open")]
    CircleOpen,

    /// <summary>Draws the circle dot marker symbol.</summary>
    [JsonStringEnumMemberName("circle-dot")]
    CircleDot,

    /// <summary>Draws the circle open dot marker symbol.</summary>
    [JsonStringEnumMemberName("circle-open-dot")]
    CirleOpenDot,

    /// <summary>Draws the square marker symbol.</summary>
    [JsonStringEnumMemberName("square")]
    Square,

    /// <summary>Draws the square open marker symbol.</summary>
    [JsonStringEnumMemberName("square-open")]
    SquareOpen,

    /// <summary>Draws the square dot marker symbol.</summary>
    [JsonStringEnumMemberName("square-dot")]
    SquareDot,

    /// <summary>Draws the square open dot marker symbol.</summary>
    [JsonStringEnumMemberName("square-open-dot")]
    SquareOpenDot,

    /// <summary>Draws the diamond marker symbol.</summary>
    [JsonStringEnumMemberName("diamond")]
    Diamond,

    /// <summary>Draws the diamond open marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-open")]
    DiamondOpen,

    /// <summary>Draws the diamond dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-dot")]
    DiamondDot,

    /// <summary>Draws the diamond open dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-open-dot")]
    DiamondOpenDot,

    /// <summary>Draws the cross marker symbol.</summary>
    [JsonStringEnumMemberName("cross")]
    Cross,

    /// <summary>Draws the cross open marker symbol.</summary>
    [JsonStringEnumMemberName("cross-open")]
    CrossOpen,

    /// <summary>Draws the cross dot marker symbol.</summary>
    [JsonStringEnumMemberName("cross-dot")]
    CrossDot,

    /// <summary>Draws the cross open dot marker symbol.</summary>
    [JsonStringEnumMemberName("cross-open-dot")]
    CrossOpenDot,

    /// <summary>Draws the x marker symbol.</summary>
    [JsonStringEnumMemberName("x")]
    X,

    /// <summary>Draws the x open marker symbol.</summary>
    [JsonStringEnumMemberName("x-open")]
    XOpen,

    /// <summary>Draws the x dot marker symbol.</summary>
    [JsonStringEnumMemberName("x-dot")]
    XDot,

    /// <summary>Draws the x open dot marker symbol.</summary>
    [JsonStringEnumMemberName("x-open-dot")]
    XOpenDot,

    /// <summary>Draws the triangle up marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-up")]
    TriangleUp,

    /// <summary>Draws the triangle up open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-up-open")]
    TriangleUpOpen,

    /// <summary>Draws the triangle up dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-up-dot")]
    TriangleUpDot,

    /// <summary>Draws the triangle up open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-up-open-dot")]
    TriangleUpOpenDot,

    /// <summary>Draws the triangle down marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-down")]
    TriangleDown,

    /// <summary>Draws the triangle down open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-down-open")]
    TriangleDownOpen,

    /// <summary>Draws the triangle down dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-down-dot")]
    TriangleDownDot,

    /// <summary>Draws the triangle down open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-down-open-dot")]
    TriangleDownOpenDot,

    /// <summary>Draws the triangle left marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-left")]
    TriangleLeft,

    /// <summary>Draws the triangle left open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-left-open")]
    TriangleLeftOpen,

    /// <summary>Draws the triangle left dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-left-dot")]
    TriangleLeftDot,

    /// <summary>Draws the triangle left open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-left-open-dot")]
    TriangleLeftOpenDot,

    /// <summary>Draws the triangle right marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-right")]
    TriangleRight,

    /// <summary>Draws the triangle right open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-right-open")]
    TriangleRightOpen,

    /// <summary>Draws the triangle right dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-right-dot")]
    TriangleRightDot,

    /// <summary>Draws the triangle right open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-right-open-dot")]
    TriangleRightOpenDot,

    /// <summary>Draws the triangle northeast marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northeast")]
    TriangleNorthEast,

    /// <summary>Draws the triangle northeast open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northeast-open")]
    TriangleNorthEastOpen,

    /// <summary>Draws the triangle northeast dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northeast-dot")]
    TriangleNorthEastDot,

    /// <summary>Draws the triangle northeast open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northeast-open-dot")]
    TriangleNorthEastOpenDot,

    /// <summary>Draws the triangle southeast marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southeast")]
    TriangleSouthEast,

    /// <summary>Draws the triangle southeast open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southeast-open")]
    TriangleSouthEastOpen,

    /// <summary>Draws the triangle southeast dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southeast-dot")]
    TriangleSouthEastDot,

    /// <summary>Draws the triangle southeast open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southeast-open-dot")]
    TriangleSouthEastOpenDot,

    /// <summary>Draws the triangle southwest marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southwest")]
    TriangleSouthWest,

    /// <summary>Draws the triangle southwest open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southwest-open")]
    TriangleSouthWestOpen,

    /// <summary>Draws the triangle southwest dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southwest-dot")]
    TriangleSouthWestDot,

    /// <summary>Draws the triangle southwest open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-southwest-open-dot")]
    TriangleSouthWestOpenDot,

    /// <summary>Draws the triangle northwest marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northwest")]
    TriangleNorthWest,

    /// <summary>Draws the triangle northwest open marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northwest-open")]
    TriangleNorthWestOpen,

    /// <summary>Draws the triangle northwest dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northwest-dot")]
    TriangleNorthWestDot,

    /// <summary>Draws the triangle northwest open dot marker symbol.</summary>
    [JsonStringEnumMemberName("triangle-northwest-open-dot")]
    TriangleNorthWestOpenDot,

    /// <summary>Draws the pentagon marker symbol.</summary>
    [JsonStringEnumMemberName("pentagon")]
    Pentagon,

    /// <summary>Draws the pentagon open marker symbol.</summary>
    [JsonStringEnumMemberName("pentagon-open")]
    PentagonOpen,

    /// <summary>Draws the pentagon dot marker symbol.</summary>
    [JsonStringEnumMemberName("pentagon-dot")]
    PentagonDot,

    /// <summary>Draws the pentagon open dot marker symbol.</summary>
    [JsonStringEnumMemberName("pentagon-open-dot")]
    PentagonOpenDot,

    /// <summary>Draws the hexagon marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon")]
    Hexagon,

    /// <summary>Draws the hexagon open marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon-open")]
    HexagonOpen,

    /// <summary>Draws the hexagon dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon-dot")]
    HexagonDot,

    /// <summary>Draws the hexagon open dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon-open-dot")]
    HexagonOpenDot,

    /// <summary>Draws the hexagon2 marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon2")]
    Hexagon2,

    /// <summary>Draws the hexagon2 open marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon2-open")]
    Hexagon2Open,

    /// <summary>Draws the hexagon2 dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon2-dot")]
    Hexagon2Dot,

    /// <summary>Draws the hexagon2 open dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagon2-open-dot")]
    Hexagon2OpenDot,

    /// <summary>Draws the octagon marker symbol.</summary>
    [JsonStringEnumMemberName("octagon")]
    Octagon,

    /// <summary>Draws the octagon open marker symbol.</summary>
    [JsonStringEnumMemberName("octagon-open")]
    OctagonOpen,

    /// <summary>Draws the octagon dot marker symbol.</summary>
    [JsonStringEnumMemberName("octagon-dot")]
    OctagonDot,

    /// <summary>Draws the octagon open dot marker symbol.</summary>
    [JsonStringEnumMemberName("octagon-open-dot")]
    OctagonOpenDot,

    /// <summary>Draws the star marker symbol.</summary>
    [JsonStringEnumMemberName("star")]
    Star,

    /// <summary>Draws the star open marker symbol.</summary>
    [JsonStringEnumMemberName("star-open")]
    StarOpen,

    /// <summary>Draws the star dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-dot")]
    StarDot,

    /// <summary>Draws the star open dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-open-dot")]
    StarOpenDot,

    /// <summary>Draws the hexagram marker symbol.</summary>
    [JsonStringEnumMemberName("hexagram")]
    Hexagram,

    /// <summary>Draws the hexagram open marker symbol.</summary>
    [JsonStringEnumMemberName("hexagram-open")]
    HexagramOpen,

    /// <summary>Draws the hexagram dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagram-dot")]
    HexagramDot,

    /// <summary>Draws the hexagram open dot marker symbol.</summary>
    [JsonStringEnumMemberName("hexagram-open-dot")]
    HexagramOpenDot,

    /// <summary>Draws the star triangle up marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-up")]
    StarTriangleUp,

    /// <summary>Draws the star triangle up open marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-up-open")]
    StarTriangleUpOpen,

    /// <summary>Draws the star triangle up dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-up-dot")]
    StarTriangleUpDot,

    /// <summary>Draws the star triangle up open dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-up-open-dot")]
    StarTriangleUpOpenDot,

    /// <summary>Draws the star triangle down marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-down")]
    StarTriangleDown,

    /// <summary>Draws the star triangle down open marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-down-open")]
    StarTriangleDownOpen,

    /// <summary>Draws the star triangle down dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-down-dot")]
    StarTriangleDownDot,

    /// <summary>Draws the star triangle down open dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-triangle-down-open-dot")]
    StarTriangleDownOpenDot,

    /// <summary>Draws the star square marker symbol.</summary>
    [JsonStringEnumMemberName("star-square")]
    StarSquare,

    /// <summary>Draws the star square open marker symbol.</summary>
    [JsonStringEnumMemberName("star-square-open")]
    StarSquareOpen,

    /// <summary>Draws the star square dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-square-dot")]
    StarSquareDot,

    /// <summary>Draws the star square open dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-square-open-dot")]
    StarSquareOpenDot,

    /// <summary>Draws the star diamond marker symbol.</summary>
    [JsonStringEnumMemberName("star-diamond")]
    StarDiamond,

    /// <summary>Draws the star diamond open marker symbol.</summary>
    [JsonStringEnumMemberName("star-diamond-open")]
    StarDiamondOpen,

    /// <summary>Draws the star diamond dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-diamond-dot")]
    StarDiamondDot,

    /// <summary>Draws the star diamond open dot marker symbol.</summary>
    [JsonStringEnumMemberName("star-diamond-open-dot")]
    StarDiamondOpenDot,

    /// <summary>Draws the diamond tall marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-tall")]
    DiamondTall,

    /// <summary>Draws the diamond tall open marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-tall-open")]
    DiamondTallOpen,

    /// <summary>Draws the diamond tall dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-tall-dot")]
    DiamondTallDot,

    /// <summary>Draws the diamond tall open dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-tall-open-dot")]
    DiamondTallOpenDot,

    /// <summary>Draws the diamond wide marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-wide")]
    DiamondWide,

    /// <summary>Draws the diamond wide open marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-wide-open")]
    DiamondWideOpen,

    /// <summary>Draws the diamond wide dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-wide-dot")]
    DiamondWideDot,

    /// <summary>Draws the diamond wide open dot marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-wide-open-dot")]
    DiamondWideOpenDot,

    /// <summary>Draws the hourglass marker symbol.</summary>
    [JsonStringEnumMemberName("hourglass")]
    Hourglass,

    /// <summary>Draws the hourglass open marker symbol.</summary>
    [JsonStringEnumMemberName("hourglass-open")]
    HourglassOpen,

    /// <summary>Draws the bowtie marker symbol.</summary>
    [JsonStringEnumMemberName("bowtie")]
    Bowtie,

    /// <summary>Draws the bowtie open marker symbol.</summary>
    [JsonStringEnumMemberName("bowtie-open")]
    BowtieOpen,

    /// <summary>Draws the circle cross marker symbol.</summary>
    [JsonStringEnumMemberName("circle-cross")]
    CircleCross,

    /// <summary>Draws the circle cross open marker symbol.</summary>
    [JsonStringEnumMemberName("circle-cross-open")]
    CircleCrossOpen,

    /// <summary>Draws the circle x marker symbol.</summary>
    [JsonStringEnumMemberName("circle-x")]
    CircleX,

    /// <summary>Draws the circle x open marker symbol.</summary>
    [JsonStringEnumMemberName("circle-x-open")]
    CircleXOpen,

    /// <summary>Draws the square cross marker symbol.</summary>
    [JsonStringEnumMemberName("square-cross")]
    SquareCross,

    /// <summary>Draws the square cross open marker symbol.</summary>
    [JsonStringEnumMemberName("square-cross-open")]
    SquareCrossOpen,

    /// <summary>Draws the square x marker symbol.</summary>
    [JsonStringEnumMemberName("square-x")]
    SquareX,

    /// <summary>Draws the square x open marker symbol.</summary>
    [JsonStringEnumMemberName("square-x-open")]
    SquareXOpen,

    /// <summary>Draws the diamond cross marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-cross")]
    DiamondCross,

    /// <summary>Draws the diamond cross open marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-cross-open")]
    DiamondCrossOpen,

    /// <summary>Draws the diamond x marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-x")]
    DiamondX,

    /// <summary>Draws the diamond x open marker symbol.</summary>
    [JsonStringEnumMemberName("diamond-x-open")]
    DiamondXOpen,

    /// <summary>Draws the cross thin marker symbol.</summary>
    [JsonStringEnumMemberName("cross-thin")]
    CrossThin,

    /// <summary>Draws the cross thin open marker symbol.</summary>
    [JsonStringEnumMemberName("cross-thin-open")]
    CrossThinOpen,

    /// <summary>Draws the x thin marker symbol.</summary>
    [JsonStringEnumMemberName("x-thin")]
    XThin,

    /// <summary>Draws the x thin open marker symbol.</summary>
    [JsonStringEnumMemberName("x-thin-open")]
    XThinOpen,

    /// <summary>Draws the asterisk marker symbol.</summary>
    [JsonStringEnumMemberName("asterisk")]
    Asterisk,

    /// <summary>Draws the asterisk open marker symbol.</summary>
    [JsonStringEnumMemberName("asterisk-open")]
    AsteriskOpen,

    /// <summary>Draws the hash marker symbol.</summary>
    [JsonStringEnumMemberName("hash")]
    Hash,

    /// <summary>Draws the hash open marker symbol.</summary>
    [JsonStringEnumMemberName("hash-open")]
    HashOpen,

    /// <summary>Draws the hash dot marker symbol.</summary>
    [JsonStringEnumMemberName("hash-dot")]
    HashDot,

    /// <summary>Draws the hash open dot marker symbol.</summary>
    [JsonStringEnumMemberName("hash-open-dot")]
    HashOpenDot,

    /// <summary>Draws the y up marker symbol.</summary>
    [JsonStringEnumMemberName("y-up")]
    YUp,

    /// <summary>Draws the y up open marker symbol.</summary>
    [JsonStringEnumMemberName("y-up-open")]
    YUpOpen,

    /// <summary>Draws the y down marker symbol.</summary>
    [JsonStringEnumMemberName("y-down")]
    YDown,

    /// <summary>Draws the y down open marker symbol.</summary>
    [JsonStringEnumMemberName("y-down-open")]
    YDownOpen,

    /// <summary>Draws the y left marker symbol.</summary>
    [JsonStringEnumMemberName("y-left")]
    YLeft,

    /// <summary>Draws the y left open marker symbol.</summary>
    [JsonStringEnumMemberName("y-left-open")]
    YLeftOpen,

    /// <summary>Draws the y right marker symbol.</summary>
    [JsonStringEnumMemberName("y-right")]
    YRight,

    /// <summary>Draws the y right open marker symbol.</summary>
    [JsonStringEnumMemberName("y-right-open")]
    YRightOpen,

    /// <summary>Draws the line east west marker symbol.</summary>
    [JsonStringEnumMemberName("line-east-west")]
    LineEastWest,

    /// <summary>Draws the line east west open marker symbol.</summary>
    [JsonStringEnumMemberName("line-east-west-open")]
    LineEastWestOpen,

    /// <summary>Draws the line north south marker symbol.</summary>
    [JsonStringEnumMemberName("line-north-south")]
    LineNorthSouth,

    /// <summary>Draws the line north south open marker symbol.</summary>
    [JsonStringEnumMemberName("line-north-south-open")]
    LineNorthSouthOpen,

    /// <summary>Draws the line northeast marker symbol.</summary>
    [JsonStringEnumMemberName("line-northeast")]
    LineNorthEast,

    /// <summary>Draws the line northeast open marker symbol.</summary>
    [JsonStringEnumMemberName("line-northeast-open")]
    LineNorthEastOpen,

    /// <summary>Draws the line northwest marker symbol.</summary>
    [JsonStringEnumMemberName("line-northwest")]
    LineNorthWest,

    /// <summary>Draws the line northwest open marker symbol.</summary>
    [JsonStringEnumMemberName("line-northwest-open")]
    LineNorthWestOpen,

    /// <summary>Draws the arrow up marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-up")]
    ArrowUp,

    /// <summary>Draws the arrow up open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-up-open")]
    ArrowUpOpen,

    /// <summary>Draws the arrow down marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-down")]
    ArrowDown,

    /// <summary>Draws the arrow down open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-down-open")]
    ArrowDownOpen,

    /// <summary>Draws the arrow left marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-left")]
    ArrowLeft,

    /// <summary>Draws the arrow left open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-left-open")]
    ArrowLeftOpen,

    /// <summary>Draws the arrow right marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-right")]
    ArrowRight,

    /// <summary>Draws the arrow right open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-right-open")]
    ArrowRightOpen,

    /// <summary>Draws the arrow bar up marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-up")]
    ArrowBarUp,

    /// <summary>Draws the arrow bar up open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-up-open")]
    ArrowBarUpOpen,

    /// <summary>Draws the arrow bar down marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-down")]
    ArrowBarDown,

    /// <summary>Draws the arrow bar down open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-down-open")]
    ArrowBarDownOpen,

    /// <summary>Draws the arrow bar left marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-left")]
    ArrowBarLeft,

    /// <summary>Draws the arrow bar left open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-left-open")]
    ArrowBarLeftOpen,

    /// <summary>Draws the arrow bar right marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-right")]
    ArrowBarRight,

    /// <summary>Draws the arrow bar right open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-bar-right-open")]
    ArrowBarRightOpen,

    /// <summary>Draws the arrow marker symbol.</summary>
    [JsonStringEnumMemberName("arrow")]
    Arrow,

    /// <summary>Draws the arrow open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-open")]
    ArrowOpen,

    /// <summary>Draws the arrow wide marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-wide")]
    ArrowWide,

    /// <summary>Draws the arrow wide open marker symbol.</summary>
    [JsonStringEnumMemberName("arrow-wide-open")]
    ArrowWideOpen
}
