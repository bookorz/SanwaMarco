Function RobotPut();
    Return ("Destination Empty", "'@<arg1>' = 'undefined'");
    '設定點位(方便閱讀，拆成5行)
	SETVAR("@point", DECODE("@<arg1>", "P1", "0001", "P2", "0002", "P3", "0003", "P4", "0004", "@<point>"));
	SETVAR("@point", DECODE("@<arg1>", "BF11", "0011", "BF12", "0012", "BF13", "0013", "BF14", "0014", "BF15", "0015", "BF16", "0016", "BF17", "0017", "@<point>"));
	SETVAR("@point", DECODE("@<arg1>", "BF21", "0021", "BF22", "0022", "BF23", "0023", "BF24", "0024", "BF25", "0025", "BF26", "0026", "BF27", "0027", "@<point>"));
	SETVAR("@point", DECODE("@<arg1>", "BF31", "0031", "BF32", "0032", "BF33", "0033", "BF34", "0034", "BF35", "0035", "BF36", "0036", "@<point>"));
	SETVAR("@point", DECODE("@<arg1>", "BF41", "0041", "BF42", "0042", "BF43", "0043", "BF44", "0044", "BF45", "0045", "BF46", "0046", "@<point>"));
	Return ("point_unknow,@<arg1>", "'@<point>' = 'undefined'");
	
	'檢查來源地在席 io Sensor
	SETVAR("@io", DECODE("@<arg1>", "P1", 101;102;103;108;109;110;106;107, "P2", 201;202;203;111;112;113;206;207, "P3", 301;302;303;305, "P4", 309;310;311;313, "@<io>"));
	SETVAR("@io", DECODE("@<arg1>", "BF11", 500;501, "BF12", 504;505, "BF13", 508;509, "BF14", 512;513, "BF15", 808;809, "BF16", 800;801, "BF17", 804;805, "@<io>"));
	SETVAR("@io", DECODE("@<arg1>", "BF21", 502;503, "BF22", 506;507, "BF23", 510;511, "BF24", 514;515, "BF25", 810;811, "BF26", 802;803, "BF27", 806;807, "@<io>"));
	SETVAR("@io", DECODE("@<arg1>", "BF31", 600;601, "BF32", 604;605, "BF33", 608;609, "BF34", 2200;2201, "BF35", 2204;2205, "BF36", 2208;2209, "@<io>"));
	SETVAR("@io", DECODE("@<arg1>", "BF41", 602;603, "BF42", 606;607, "BF43", 610;611, "BF44", 2202;2203, "BF45", 2206;2207, "BF46", 2210;2211, "@<io>"));
	Return ("sensor_unknow,@<arg1>", "'@<io>' = 'undefined'");
	'檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1");
	IF ("'@<arg1>' in ('P1','P2')")
        SETVAR("@values", "11100101");
	ELSEIF("'@<arg1>' in ('P3','P4')")
        SETVAR("@values", "1111");
	ELSE
        SETVAR("@values", "11");
	ENDIF
    'RETURN("io sensors @<io> need value:@<values>");
    '檢查IO
    SETVAR("@device", "DNM01");
    API("I7565DNM_CHECK_IOS", True);
	
	'執行PUT
	SETVAR("@device", "Robot01");
	SETVAR("@cmd", "$1CMD:PUT__:@<point>,001,1,0");
    API("ATEL_ROBOT_MOTION_CMD", True);	
	
	'放完後檢查實際IO
	SETVAR("@interval", "100");
	SETVAR("@retry_count", "200");
	IF ("'@<arg1>' in ('P1','P2')")
        SETVAR("@values", "00000101");
	ELSEIF("'@<arg1>' in ('P3','P4')")
        SETVAR("@values", "0001");
	ELSE
        SETVAR("@values", "00");
	ENDIF
    SETVAR("@device", "DNM01");
    API("I7565DNM_CHECK_IOS", True);
	
End Function;