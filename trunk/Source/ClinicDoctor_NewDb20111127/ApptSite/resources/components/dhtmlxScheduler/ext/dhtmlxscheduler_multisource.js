/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
(function(){

	function backup(obj){
		var t = function(){};
		t.prototype = obj;
		return t;
	}

	var old = scheduler._load;
	scheduler._load=function(url,from){
		url=url||this._load_url;
		if (typeof url == "object"){
			var t = backup(this._loaded);
			for (var i=0; i < url.length; i++) {
				this._loaded=new t();
				old.call(this,url[i],from);
			}
		} else 
			old.apply(this,arguments);
	}
	
})();