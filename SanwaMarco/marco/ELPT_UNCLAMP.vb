Function ELPT_UNCLAMP();
    SETVAR("@device", "DNM01");
    SETVAR("@io", DECODE("@<arg1>", "P1", "900;901", "P2", "902;903", "@<point>"));
    SETVAR("@value", "0;1");
    API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    SETVAR("@io", DECODE("@<arg1>", "P1", "104;105;106;107", "P2", "204;205;206;207", "@<io>"));
    '檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "30");
    SETVAR("@values", "0101");
    API("I7565DNM_CHECK_IOS", False);
End Function;