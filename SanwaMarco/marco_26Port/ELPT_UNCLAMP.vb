Function ELPT_UNCLAMP();
    SETVAR("@device", "DNM01");
    SETVAR("@io", DECODE("@<arg1>", "P1", "900;901", "P2", "902;903", "@<point>"));
    SETVAR("@value", "0;1");
    API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    'SETVAR("@io", DECODE("@<arg1>", "P1", "104;105;106;107", "P2", "204;205;206;207", "@<io>")); 左夾爪相關Sensor移除
    SETVAR("@io", DECODE("@<arg1>", "P1", "106;107", "P2", "206;207", "@<io>"));
    '檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "150");
    'SETVAR("@values", "0101"); 左夾爪相關Sensor移除
    SETVAR("@values", "01");
    API("I7565DNM_CHECK_IOS", True);
End Function;