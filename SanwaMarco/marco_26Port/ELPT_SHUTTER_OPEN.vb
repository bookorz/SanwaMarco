﻿Function ELPT_SHUTTER_OPEN();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "('@<arg1>' <> 'P1') AND ('@<arg1>' <> 'P2')");
    'CHECK READY
    SETVAR("@io", DECODE("@<arg1>", "P1", "2002", "P2", "2102", "@<io>"));
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1");
    SETVAR("@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("Port not Ready.", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    '26Port 形式不包含AREA SENSOR 
    'CHECK A AREA SENSOR
    'SETVAR("@io", "710");
	'SETVAR("@interval", "200");
	'SETVAR("@retry_count", "1");
    'SETVAR("@values", "1");
    'API("I7565DNM_CHECK_IOS", False);
    'Return ("A AREA SENSOR ON", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'OPEN SHUTTER
    SETVAR("@io", DECODE("@<arg1>", "P1", "905;904", "P2", "907;906", "@<io>"));
    SETVAR("@value", "0;1");
    API("I7565DNM_SETIOS", True);
    'CHECK SHUTTER
    SETVAR("@io", DECODE("@<arg1>", "P1", "704;705", "P2", "706;707"));
	SETVARS("@interval", "200", "@retry_count", "50", "@values", "10");
    '26Port 形式不包含AREA SENSOR 
    '新增緊急停止功能
    'SETVAR("@ems_input", "710");
    'SETVAR("@ems_values", "1");
    '關閉 A1-SERVO(OFF) A2-SERVO(OFF) A1 Shutter開(OFF) A1 Shutter關(OFF) A2 Shutter開(OFF) A2 Shutter關(OFF)    
    'SETVAR("@ems_output", "2009;2109;904;905;906;907");
    'SETVAR("@ems_enabled", "0;0;0;0;0;0");
    'API("I7565DNM_CHECK_IOS", False);
    'Return ("A AREA SENSOR ON", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    API("I7565DNM_CHECK_IOS", True);
    'Clear Command
    SETVAR("@io", DECODE("@<arg1>", "P1", "905;904", "P2", "907;906", "@<io>"));
    SETVAR("@value", "0;0");
    API("I7565DNM_SETIOS", True);
End Function;