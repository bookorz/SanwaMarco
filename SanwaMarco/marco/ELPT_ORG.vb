Function ELPT_MOVEIN();
    SETVAR("@device", "DNM01");
    'CHECK READY
    SETVAR("@io", DECODE("@<arg1>", "P1", "2002", "P2", "2102", "@<io>"));
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1");
    SETVAR("@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("Port not Ready.", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'CHECK SHUTTER
    SETVAR("@io", DECODE("@<arg1>", "P1", "704", "P2", "706"));
	SETVARS("@interval", "200", "@retry_count", "1", "@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("Shutter not open.", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'SET SERVO OFF
    SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2109", "@<io>"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'SERVO ON 
    DELAY(50);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2109", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    'SET ORG
    DELAY(50);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2008", "P2", "2108", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    SETVAR("@io", DECODE("@<arg1>", "P1", "108;109;110;2000", "P2", "111;112;113;2100", "@<io>"));
    '檢查實際IO, 每1秒檢查一次, 共檢查200次
	SETVAR("@interval", "1000");
	SETVAR("@retry_count", "200");
    SETVAR("@values", "0001");
    API("I7565DNM_CHECK_IOS", True);
    'SET START = 0 & ORG = 0   
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011;2008", "P2", "2111;2108", "@<io>"));
    SETVAR("@value", "0;0");
    API("I7565DNM_SETIOS", True);
End Function;