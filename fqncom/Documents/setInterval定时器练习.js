
////设置定时器的方法
//var direction = 'left';
//setInterval(function () {

//    var tit = document.title
//    if (direction == 'left') {

//        document.title = tit.substr(1) + tit.charAt(0);
//    } else {
//        document.title = tit.charAt(tit.length - 1) + tit.substr(0, tit.length - 1);
//    }

//}, 1000);
//function setLeft() {
//    direction = 'left';
//}
//function serRight() {
//    direction = 'right';
//}
//window.onload = function () {

//    document.getElementById('b1').onclick = setLeft;

//    document.getElementById('b2').onclick = serRight;
//}



//自定义设置跑马灯效果
//设置全局变量跑马灯前进方向的默认值,定义初始方向
var direction = 'left';
//设置定时器执行的方法
function setTextByTime() {
    var text = document.title;
    if (direction == 'left') {
        //如果向左走，将字符串第二个位置开始的字符串放到第一个字符的前面。也就是将第一个字符放到最后去。
        document.title = text.substr(1) + text.charAt(0);
    } else {
        //如果向右走，将字符串的最后一个字符放到最前面来，同时加上剩下的字符串
        document.title = text.charAt(text.length - 1) + text.substr(0, text.length - 1);
    }
}
//设置定时器=============将方法进行传递的时候是当作字符串形式进行传递的！！！！
var moveUpId = setInterval('setTextByTime()', 1000) ;

//设置改变方向的值的方法
function goLeft() {
    direction = 'left';
}
function goRight() {
    direction = 'right';
}
//将设置方向的方法在页面全部加载完毕之后，注册到按钮点击事件上去
window.onload = function () {
    document.getElementById('btnLeft').onclick = goLeft;
    document.getElementById('btnRight').onclick = goRight;
    setInterval('setTextByTime()', 1000);
    //点击按钮清空定时器===========这一行的清除没用！！！！！！！！
    document.getElementById('btnRemove').onclick = clearInterval(moveUpId);
}