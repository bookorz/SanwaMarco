Function ELPT_MOVEIN();
    SETVAR("@device", "DNM01");
    'SET SERVO ON &  START = 0
    'SETVAR("@io", DECODE("@<arg1>", "P1", "2009;2011;2012;2013", "P2", "2109;2111;2112;2113", "@<io>"));
    'SETVAR("@value", "0;0;1;0");
    'SETVAR("@io", DECODE("@<arg1>", "P1", "2009;2011", "P2", "2109;2111", "@<io>"));
    'SETVAR("@value", "1;0"); 
    'CHECK SERVO
    SETVAR("@io", DECODE("@<arg1>", "P1", "2003", "P2", "2103", "@<io>"));
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "10");
    SETVAR("@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("PLEASE SERVO ON", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'SET START 0
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011", "P2", "2111", "@<io>"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'SET POSITION
    'DELAY(200);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2012;2013", "P2", "2112;2113", "@<io>"));
    SETVAR("@value", "1;0");
    API("I7565DNM_SETIOS", True);
    'START & SERVO ON 
    DELAY(500);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011", "P2", "2111", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    'DELAY(200);
    'SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2109", "@<io>"));
    'SETVAR("@value", "1");
    'API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    SETVAR("@io", DECODE("@<arg1>", "P1", "108;109;110", "P2", "111;112;113", "@<io>"));
    '檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1000");
    SETVAR("@values", "100");
    API("I7565DNM_CHECK_IOS", True);
End Function;