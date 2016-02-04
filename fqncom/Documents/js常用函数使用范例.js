//js函数集
//•字符串(String) 
//1.声明 
//var myString = new String("Every good boy does fine."); 
//声明变量
var myString = new String('you are awesome');
//var myString = "Every good boy does fine.";
//也可以直接给变量赋值
var myString = ('of course i am');
//2.字符串连接 
//var myString = "Every " + "good boy " + "does fine."; 
//字符串连接，可以直接使用加号
var myString = 'if you are' + 'a man' + ',come out and fight for ' + 'that girl';
//var myString = "Every "; myString += "good boy does fine.";
//也可以直接当stringbuilder来使用
var myString = 'that is'; myString += ' nice of you ';
//3.截取字符串 
////截取第 6 位开始的字符 
//var myString = "Every good boy does fine."; 
//var section = myString.substring(6); //结果: "good boy does fine."
//可以直接截取字符串
var section = myString.substring(6);//从第6个开始截取，包括第六个
////截取第 0 位开始至第 10 位为止的字符 
//var myString = "Every good boy does fine."; 
//var section = myString.substring(0,10); //结果: "Every good"
var section = myString.substring(0, 10);//返回的是从第0个位置开始截取，截取到第十位的字符（不包括第十位的字符）
////截取从第 11 位到倒数第 6 位为止的字符 
//var myString = "Every good boy does fine."; 
//var section = myString.slice(11,-6); //结果: "boy does"
var section = myString.slice(11, -6);//表示从第十一位开始截取，直到截取到倒数第六位为止
////从第 6 位开始截取长度为 4 的字符 
//var myString = "Every good boy does fine."; 
//var section = myString.substr(6,4); //结果: "good"
var section = myString.substr(6, 4);//表示从第六位开始截取，截取的长度是4的字符串，包括第六位的字符
var section = myString.substr(6);//表示从第六位开始截取，直到最后一位截至
//4.转换大小写 
//var myString = "Hello"; 
//var lcString = myString.toLowerCase(); //结果: "hello" 
var str = myString.toLowerCase();//全部转成小写
//var ucString = myString.toUpperCase(); //结果: "HELLO"
var str = myString.toUpperCase();//全部转成大写
//5.字符串比较 
//var aString = "Hello!"; 
//var bString = new String("Hello!"); 
//if( aString == "Hello!" ){ } //结果: true 
//字符串中间进行比较，首先两个等于号表示‘值’等于，而三个等号表示不仅‘值’等于，而且其‘类型’也相等
var str1 = 'hahaha';
var str2 = 'hahaha';
if (str1 == str2) { };//此处返回结果是true，因为两个字符串比较的是值是否相等
if (str1 === str2) { };//此处返回的是false，因为两个字符串比较的是类型是否相等
//if( aString == bString ){ } //结果: true 
//if( aString === bString ){ } //结果: false (两个对象不同,尽管它们的值相同)
//6.检索字符串 
//var myString = "hello everybody."; 
//// 如果检索不到会返回-1,检索到的话返回在该串中的起始位置 
//if( myString.indexOf("every") > -1 ){ } //结果: true
myString.indexOf('heheh');//此处indexOf和C#里的一致，如果检索到则返回是第一次出现时的索引位置，否则返回的是-1，表示没有检索到
//7.查找替换字符串 
//var myString = "I is your father."; 
//var result = myString.replace("is","am"); //结果: "I am your father."
myString.replace('hehe', 'haha');//表示的是去字符串中找，去匹配，如果匹配到了则进行替换，否则不做任何操作。
//8.特殊字符: 
//\b : 后退符 \t : 水平制表符 
//\n : 换行符 \v : 垂直制表符 
//\f : 分页符 \r : 回车符 
//\" : 双引号 \' : 单引号 
//\\ : 反斜杆
//9.将字符转换成Unicode编码 
//var myString = "hello"; 
//var code = myString.charCodeAt(3); //返回"l"的Unicode编码(整型) 
var code = myString.charCodeAt(3);//表示找到索引为3的字符，然后返回他的Unicode编码（整数形式）
//var char = String.fromCharCode(66); //返回Unicode为66的字符
var code = String.fromCharCode(56);//表示返回括号内unicode为56的字符
//10.将字符串转换成URL编码 
//var myString = "hello all"; 
//var code = encodeURI(myString); //结果: "hello%20all" 
var code = encodeURI(myString);//将字符串转换成uri编码，=============不是很理解===========
//var str = decodeURI(code); //结果: "hello all" 
var str = decodeURI(myString);//目的是解析uri代码
////相应的还有: encodeURIComponent() decodeURIComponent()
//11.将字符串转换成base64编码 
//// base64Encode() base64Decode() 用法同上
////-----------------------------------------------------------------------
//•数字型(Number) 
//1.声明 
//var i = 1; 
//var i = new Number(1);
//2.字符串与数字间的转换 
//var i = 1; 
//var str = i.toString(); //结果: "1" 
//var str = new String(i); //结果: "1" 
var num = 11;
var str = num.toString();
var str = new String(num);//数字转字符串
//i = parseInt(str); //结果: 1 
//i = parseFloat(str); //结果: 1.0
num = parseInt(str);
num = parseFloat(str);//字符串转数字，当字符串中含有数字时，只要一开始是数字，之后一旦遇到非数字，马上返回前面的数字部分，如果一开始就是非数字，那么就算中间有数字也没用
num = Number(str);//当字符串中含有非数字时，返回的是NaN，即not a number 非数字
////注意: parseInt,parseFloat会把一个类似于"32G"的字符串,强制转换成32
//3.判断是否为有效的数字 
//var i = 123; var str = "string"; 
//if( typeof i == "number" ){ } //true
var num = 123;
if (typeof num == 'number') { };//判断类型是否是数字，返回true
////某些方法(如:parseInt,parseFloat)会返回一个特殊的值NaN(Not a Number) 
////请注意第2点中的[注意],此方法不完全适合判断一个字符串是否是数字型!! 
//i = parseInt(str); 
//if( isNaN(i) ){ }
var num = String(str);
if (isNaN(num)) { };//判断转换之后的 是否 不是数字，如果不是数字，则返回true，
//4.数字型比较 
////此知识与[字符串比较]相同
//5.小数转整数 
//var f = 1.5; 
//var i = Math.round(f); //结果:2 (四舍五入) 
var num = Math.round(1.3);//math.round()进行的是四舍五入的计算,返回1
//var i = Math.ceil(f); //结果:2 (返回大于f的最小整数) 
var num = Math.ceil(1.2);//和数据库里的ceiling一致，向上进位，返回2.ceil表示天花板
//var i = Math.floor(f); //结果:1 (返回小于f的最大整数)
var num = Math.floor(1.7);//返回比他小的整数，floor，地板的意思，和ceil相反。很好理解，返回1
//6.格式化显示数字 
//var i = 3.14159;
////格式化为两位小数的浮点数 
//var str = i.toFixed(2); //结果: "3.14"
var num1 = 3.1415926;
var num2 = num1.toFixed(3);//返回保留三位小数的数，即3.14  ==========会不会涉及到进位的问题,会涉及到，会进位，和有效数字一致=========
////格式化为五位数字的浮点数(从左到右五位数字,不够补零) 
//var str = i.toPrecision(5); //结果: "3.1415"
var num2 = num1.toPrecision(5);//返回数字个数一共是5位的数字。返回的是有效数字，测试0004562.236500

////////////////////////////////7.X进制数字的转换 
//////////////////////////////////不是很懂 -.- 
////////////////////////////////var i = parseInt("0x1f",16); 
////////////////////////////////var i = parseInt(i,10); 
////////////////////////////////var i = parseInt("11010011",2);

//8.随机数 
////返回0-1之间的任意小数 
//var rnd = Math.random(); 
var rnd = Math.random();//返回的是0-1之间的随机数，可以使用乘法进行更多的随机数产生
////返回0-n之间的任意整数(不包括n) 
//var rnd = Math.floor(Math.random() * n)
var rnd = Math.floor(math.random() * n);//表示将产生的1-0之间的随机数进行乘以一个数，然后得到得以可能带小数的随机数，然后使用floor方法向下取整。得到整数形式的随机数，所以不包括n这个随机数。
////-----------------------------------------------------------------------
//•Math对象 
//1. Math.abs(num) : 返回num的绝对值 
Math.abs(num);//取绝对值
//2. Math.acos(num) : 返回num的反余弦值 
//3. Math.asin(num) : 返回num的反正弦值 
//4. Math.atan(num) : 返回num的反正切值 
//5. Math.atan2(y,x) : 返回y除以x的商的反正切值 
//6. Math.ceil(num) : 返回大于num的最小整数 
Math.ceil(num);//向上取整
//7. Math.cos(num) : 返回num的余弦值 
//8. Math.exp(x) : 返回以自然数为底,x次幂的数 
//9. Math.floor(num) : 返回小于num的最大整数 
Math.floor(num);//向下取整
//10.Math.log(num) : 返回num的自然对数 
//11.Math.max(num1,num2) : 返回num1和num2中较大的一个 
Math.max(num1, num2);//返回两个数中最大的那一个
//12.Math.min(num1,num2) : 返回num1和num2中较小的一个 
Math.min(num1, num2);//返回两个数中较小的那一个
//13.Math.pow(x,y) : 返回x的y次方的值 
//14.Math.random() : 返回0到1之间的一个随机数 
Math.random();//返回0-1之间的一个随机数
//15.Math.round(num) : 返回num四舍五入后的值 
Math.round();//返回四舍五入之后的值
//16.Math.sin(num) : 返回num的正弦值 
//17.Math.sqrt(num) : 返回num的平方根 
//18.Math.tan(num) : 返回num的正切值 
//19.Math.E : 自然数(2.718281828459045)
Math.E//返回自然数e的值
//20.Math.LN2 : 2的自然对数(0.6931471805599453) 
//21.Math.LN10 : 10的自然对数(2.302585092994046) 
//22.Math.LOG2E : log 2 为底的自然数(1.4426950408889634) 
//23.Math.LOG10E : log 10 为底的自然数(0.4342944819032518) 
//24.Math.PI : π(3.141592653589793) 
Math.PI;//返回π的值
//25.Math.SQRT1_2 : 1/2的平方根(0.7071067811865476) 
//26.Math.SQRT2 : 2的平方根(1.4142135623730951)
////-----------------------------------------------------------------------
//•日期型(Date) 
//1.声明 
//var myDate = new Date(); //系统当前时间
var date = new Date();//得到系统当前时间
//var myDate = new Date(yyyy, mm, dd, hh, mm, ss); 
var date = new Date(yyyy, mm, dd, hh, mm, ss);//根据一定格式进行时间的返回
//var myDate = new Date(yyyy, mm, dd); 
//var myDate = new Date("monthName dd, yyyy hh:mm:ss"); 
//var myDate = new Date("monthName dd, yyyy"); 
//var myDate = new Date(epochMilliseconds);
var date = new Date(epochMilliseconds);//这个还不知道有什么用。epoch：时期，阶段，新纪元的意思
//2.获取时间的某部份 
//var myDate = new Date(); 
//myDate.getYear(); //获取当前年份(2位) 
//myDate.getFullYear(); //获取完整的年份(4位,1970-????) 
//myDate.getMonth(); //获取当前月份(0-11,0代表1月) 
//myDate.getDate(); //获取当前日(1-31) 
//myDate.getDay(); //获取当前星期X(0-6,0代表星期天) 
//myDate.getTime(); //获取当前时间(从1970.1.1开始的毫秒数) 
//myDate.getHours(); //获取当前小时数(0-23) 
//myDate.getMinutes(); //获取当前分钟数(0-59) 
//myDate.getSeconds(); //获取当前秒数(0-59) 
//myDate.getMilliseconds(); //获取当前毫秒数(0-999) 
//myDate.toLocaleDateString(); //获取当前日期 
//myDate.toLocaleTimeString(); //获取当前时间 
//myDate.toLocaleString( ); //获取日期与时间
//3.计算之前或未来的时间 
//var myDate = new Date(); 
//myDate.setDate(myDate.getDate() + 10); //当前时间加10天 
date.setDate(date.getDate() + 10);
////类似的方法都基本相同,以set开头,具体参考第2点
//4.计算两个日期的偏移量 
//var i = daysBetween(beginDate,endDate); //返回天数 
var day = dayBetween(beginDate, endDate);
//var i = beginDate.getTimezoneOffset(); //返回本地时间与格林尼治时间的时间差，以分钟为单位
var minutes = beginDate.getTimezoneOffset();//
//5.检查有效日期 
////checkDate() 只允许"mm-dd-yyyy"或"mm/dd/yyyy"两种格式的日期 
//if( checkDate("2006-01-01") ){ }
////正则表达式(自己写的检查 yyyy-mm-dd, yy-mm-dd, yyyy/mm/dd, yy/mm/dd 四种) 
//var r = /^(\d{2}|\d{4})[\/-]\d{1,2}[\/-]\d{1,2}$/; 
//if( r.test( myString ) ){ }
////-----------------------------------------------------------------------
//•数组(Array) 
//1.声明 
//var arr = new Array(); //声明一个空数组 
var arr = new Array();//声明一个空数组
//var arr = new Array(10); //声明一个10个长度的数组 
var arr = new Array(20);//手动声明一个长度是20的数组，但是没什么意义。。。因为js里的数组是可变的
//var arr = new Array("Alice", "Fred", "Jean"); //用值初始化数组 
var arr = new Array('hah', 'gge', 'meme');//直接用值初始化一个数组
//var arr = ["Alice", "Fred", "Jean"]; //用值初始化数组 
var arr = ['hah', 'asd', 'sda'];//直接用值初始化数组，连构造方法都不用写
//var arr = [["A","B","C"],[1,2,3]]; //声明一个二(多)维数组
var arr = [[1, 2, 5, 4, 6], [5, 4, 8, 7]];//声明一个多维数组
//2.数组的访问 
//arr[0] = "123"; //赋值 
//var str = arr[0]; //获取 
//arr[0][0] = "123"; //多维数组赋值
//3.数组与字符串间的转换 
//var arr = ["A","B","C","D"]; //声明
////数组按分隔符转换成字符串 
//var str = arr.join("|"); //结果: "A|B|C|D"
var str = arr.join('|');//在arr数组中加入竖线然后返回一个字符串
////字符串切割成数组 
//arr = str.split("|");
var strs = str.split('|');
//4.遍历数组 
//for( var i=0; i<arr.length; i++ ){ alert(arr[i]); }
for (var i = 0; i < arr.length; i++) {
    alert(arr[i]);
}
//5.排序 
//var arr = [12,15,8,9]; 
//arr.sort(); //结果: 8 9 12 15
var arr = [1, 122, 35, 34, 56, 34];
arr.sort();//sort()方法进行了排序
//6.组合与分解数组 
//var arr1 = ["A","B","C","D"]; 
//var arr2 = ["1","2","3","4"];
////奖两个数组组合成一个新的数组 
//var arr = arr1.concat(arr2); //结果: ["A","B","C","D","1","2","3","4"]
var arrNew = arr1.concat(arr2);
////将一个数组切成两个数组(参数1:起始索引,参数2:切割长度) 
//var arr3 = arr.splice(1,3); //结果: arr3:["B","C","D"] arr["A","1","2","3","4"]
var arr3 = arr.splice(1, 3);//plice(index,length);index开始索引，length切割长度
////将一个数组切成两个数组,并在原数组补新值 
//var arr4 = arr.splice(1,3,"AA"); //结果: arr4:["B","C","D"] arr["A","AA","1","2","3","4"]
var arr4 = arr.splice(1, 3, 'AA');
////-----------------------------------------------------------------------
//•自定义对象 
//1.声明: 
//    function myUser(uid,pwd){ 
//        this.uid = uid; 
//        this.pwd = pwd || "000000"; //默认值 
//        this.show = showInfo; //方法 
//    }
function myUser(name,age) {
    this.name = name;
    this.age = age;
    this.show = function () {
        alert('hehe ');
    }
}
////下面的函数不是自定义对象,是自定义对象的方法.继续看下去就明白了 
//function showInfo(){ 
//    alert("用户名:" + this.uid + ",密码:" + this.pwd) 
//}
//2.实例化: 
//    var user = new myUser("user","123456"); 
//var user = {uid:"user",pwd:"123456"};
//3.获取与设置 
//alert("用户名是:" + user.uid); //get 
//user.uid = "newuser"; //set 
//user.show(); //调用show()方法
////-----------------------------------------------------------------------
//•变量 函数 流程控制 
//1.变量 
//var i = 1; 
//var i = 1, str = "hello";
//2.函数 
//function funName(){ 
//    //do something. 
//} 
//function funName(param1[,paramX]){ 
//    //do something. 
//}
//    3.嵌套函数 
//    //某种情况,你需要创建一个函数本身所独有的函数. 
//    function myFunction(){ 
//        //do something. 
//        privateFunction(); 
//        function privateFunction(){ 
//            //do something. 
//        } 
//    }
//    4.匿名函数 
//    var tmp = function(){ alert("only test."); } 
//    tmp();
//    5.延迟函数调用 
//    var tId = setTimeout("myFun()",1000); //延迟1000毫秒后再调用myFun()函数 
//    fucntion myFun(){ 
//        //do something 
//        clearTimeout(tId); //销毁对象 
//    }
var timeId = setTimeout(function () { }, 1000);
function doSomething() {
    alert('hehe ');
    clearTimeout(timeId);
}
//    6.流程控制 
//    if( condition ){ } 
//    if( condition ){ } else{ } 
//    if( condition ){ } else if( condition ){ } else{ }
//    switch( expression ){ ==========//注意此处是强比较，即先比较类型然后比较数值一致性
//        case valA : statement; break; 
//        case valB : statement; break; 
//        default : statement; break; 
//    }
switch (switch_on) {
    case 1: alert(); break;
    default: alert(); break;
}
////////////////////    7.异常捕获 
////////////////////    try{ expression } catch(e){ } finally{ }
////////////////////    //不处理任何异常 
////////////////////    window.onerror = doNothing; 
////////////////////    function doNothing(){ return true; }
////////////////////    //异常类可用的属性 
////////////////////    description : 异常描述(IE,NN) 
////////////////////    fileName : 异常页面URI(NN) 
////////////////////    lineNumber : 异常行数(NN) 
////////////////////    message : 异常描述(IE,NN) 
////////////////////    name : 错误类型(IE,NN) 
////////////////////    number : 错误代码(IE)
////////////////////    //错误信息(兼容所有浏览器) 
////////////////////    try{ } 
////////////////////    catch(e){ 
////////////////////        var msg = (e.message) ? e.message : e.description; 
////////////////////        alert(msg); 
////////////////////    }
//    8.加快脚本的执行速度 
//    -避免使用 eval() 函数 
//    -避免使用 with 关键字 
//    -将重复的表达式赋值精简到最小 
//    -在较大的对象中使用索引来查找数组 
//    -减少 document.write() 的使用
//    //-----------------------------------------------------------------------
//•浏览器特征( navigator ) 
//    1.浏览器名称 
//    //IE : "Microsoft Internet Explorer" 
//    //NS : "Netscape" 
//    var browserName = navigator.appName;
var browserName = navigator.appName;
//    2.浏览器版本 
//    var browserVersion = navigator.appVersion;
var browserVersion = navigator.appVersion;
//    3.客户端操作系统 
//    var isWin = ( navigator.userAgent.indexOf("Win") != -1 ); 
//    var isMac = ( navigator.userAgent.indexOf("Mac") != -1 ); 
//    var isUnix = ( navigator.userAgent.indexOf("X11") != -1 );
//    4.判断是否支持某对象,方法,属性 
//    //当一个对象,方法,属性未定义时会返回undefined或null等,这些特殊值都是false 
//    if( document.images ){ } 
//    if( document.getElementById ){ }
//    5.检查浏览器当前语言 
//    if( navigator.userLanguage ){ var l = navigator.userLanguage.toUpperCase(); }
if (navigator.userLanguage) { var lan = navigator.userLanguage.toUpperCase(); }
//    6.检查浏览器是否支持Cookies 
//    if( navigator.cookieEnabled ){ }
if (navigator.cookieEnabled) { }
//    //-----------------------------------------------------------------------
//•控制浏览器窗口( window ) 
//    1.设置浏览器的大小 
//    window.resizeTo(800, 600); //将浏览器调整到800X600大小 
//    window.resizeBy(50, -10); //在原有大小上改变增大或减小窗口大小
//    2.调整浏览器的位置 
//    window.moveTo(10, 20); //将浏览器的位置定位到X:10 Y:20 
//    window.moveBy(0, 10); //在原有位置上移动位置(偏移量)
//    3.创建一个新的窗口 
//    var win = window.open("about.htm","winName","height=300,width=400");
//    //参数 
//    alwaysLowered //始终在其它浏览器窗口的后面(NN) 
//    alwaysRaised //始终在其它浏览器窗口的前面(NN) 
//    channelMode //是否为导航模式(IE) 
//    copyhistory //复制历史记录至新开的窗口(NN) 
//    dependent //新窗口随打开它的主窗口关闭而关闭(NN) 
//    fullscreen //全屏模式(所有相关的工具栏都没有)(IE) 
//    location //是否显示地址栏(NN,IE) 
//    menubar //是否显示菜单栏(NN,IE) 
//    scrollbars //是否显示滚动条(NN,IE) 
//    status //是否显示状态栏(NN,IE) 
//    toolbar //是否显示工具栏(NN,IE) 
//    directories //是否显示链接栏(NN,IE) 
//    titlebar //是否显示标题栏(NN) 
//    hotkeys //显示菜单快捷键(NN) 
//    innerHeight //内容区域的高度(NN) 
//    innerWidth //内容区域的宽度(NN) 
//    resizable //是否可以调整大小(NN,IE) 
//    top //窗口距离桌面上边界的大小(NN,IE) 
//    left //窗口距离桌面左边界的大小(NN,IE) 
//    height //窗口高度(NN,IE) 
//    width //浏览器的宽度
//    4.与新窗口通讯 
//    win.focus(); //让新窗口获得焦点 
//    win.document.write("abc"); //在新窗口上操作 
//    win.document.close(); //结束流操作 
//    opener.close();
//    5.模式窗口 
//    window.showModelDialog("test.htm",dialogArgs,"param"); //传递对象 
//    window.showModelessDialog("test.htm",myFunction,"param"); //传递函数 
//    window.dialogArguments //对话框访问父窗口传递过来的对象 
//    window.returnValue //父窗口获取对话框返回的值
//    //参数 
//    center //窗口居中屏幕 
//    dialogHeight //窗口高度 
//    dialogWidth //窗口宽度 
//    dialogTop //窗口距离屏幕的上边距 
//    dialogLeft //窗口距离屏幕的左边距 
//    edge //边框风格(raised|sunken) 
//    help //显示帮助按钮 
//    resizable //是否可以改变窗口大小 
//    status //是否显示状态栏
//    //例子 
//    <script> 
//    function openDialog(myForm) { 
//        var result = window.showModalDialog("new.html",myForm,"center"); 
//    } 
//    </script> 
//    <form action="#" onsubmit="return false"> 
//    <input type="text" id="txtId"> 
//    <input type="button" id="btnChk" value="验证是否可用" onclick="openDialog(this.form);"> 
//    </form>
//    //另一个页面new.html 
//    <script> 
//    window.dialogArguments.btnChk.enabled = false; //将父窗口中的按钮设置为不可用 
//    //do something to check the Id. 
//    window.write("用户ID: " + window.dialogArguments.txtId.value + " 可使用!"); //获取文本框的值 
//    </script>
//    //-----------------------------------------------------------------------
//•管理框架网页( frames ) 
//    1.创建一个框架架构网页 
//    <html> 
//    <frameset rows="50, *"> 
//    <frame name="header" src="header.html"> 
//    <frame name="main" src="main.html"> 
//    </frameset> 
//    </html>
//    2.访问框架网页 
//    window.frames[i] 
//    window.frames["frameName"] 
//    window.frameName
//    window.frames["frameName"].frames["frameName2"] 
//    parent.frames["frameName"] 
//    top.frames["frameName"]
//    3.将某一页面定向到某框架 
//    <a href="new.html" target="main"> 
//    location = "new.html"; 
//    parent.frameName.location.href = "new.html"; 
//    top.frameName.location = "new.html";
//    4.强制不让其它框架引用某页面 
//    if (top != self) { 
//        top.location.href = location.href; 
//    }
//    5.更改框架的大小 
//    document.framesetName.rows = "50,*"; 
//    document.framesetName.cols = "50,*";
//    6.动态创建框架网页 
//    var frag = document.createDocumentFragment( ); 
//    var newFrame= document.createElement("frame"); 
//    newFrame.id = "header"; 
//    newFrame.name = "header"; 
//    newFrame.src="header.html" 
//    frag.appendChild(newFrame); 
//    newFrame = document.createElement("frame"); 
//    newFrame.id = "main"; 
//    newFrame.name = "main"; 
//    newFrame.src="main.html" 
//    frag.appendChild(newFrame); 
//    document.getElementById("masterFrameset").rows = "50,*";

