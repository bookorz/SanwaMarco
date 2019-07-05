Function ELPT_MOVEIN();
    SETVAR("@device", "DNM01");
    'SET SERVO OFF &  START = 0
    SETVAR("@io", DECODE("@<arg1>", "P1", "2009;2011", "P2", "2119;2111", "@<io>"));
    SETVAR("@value", "0;0");
    API("I7565DNM_SETIOS", True);
    'SET POSITION
    DELAY(200);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2012;2013", "P2", "2112;2113", "@<io>"));
    SETVAR("@value", "1;0");
    API("I7565DNM_SETIOS", True);
    'SERVO ON & START
    DELAY(200);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011", "P2", "2111", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(200);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2119", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    SETVAR("@io", DECODE("@<arg1>", "P1", "108;109;110", "P2", "111;112;113", "@<io>"));
    '檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "30");
    SETVAR("@values", "100");
    API("I7565DNM_CHECK_IOS", True);
End Function;