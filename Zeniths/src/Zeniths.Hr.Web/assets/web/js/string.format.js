if (!String.prototype.format) {
  String.prototype.format = function() {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function(match, number) { 
      return typeof args[number] != 'undefined'
        ? args[number]
        : match
      ;
    });
  };
}

/*
var demo = '你好,我是{0},性别:{1},今年{2}岁.别忘了我是{0}.'.format('张三','男',19);
*/