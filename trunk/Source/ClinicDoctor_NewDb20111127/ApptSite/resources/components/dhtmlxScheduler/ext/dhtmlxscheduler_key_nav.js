/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
//Initial idea and implementation by Steve MC
(function (){

var isLightboxOpen = false;
var date; // used for copy and paste operations
var isCopy = null;

scheduler.attachEvent("onBeforeLightbox",function(){ isLightboxOpen = true; return true; });
scheduler.attachEvent("onAfterLightbox",function(){ isLightboxOpen = false; return true; });

scheduler.attachEvent("onMouseMove", function(id,e){
	date = scheduler.getActionData(e).date;
});

dhtmlxEvent(document,(_isOpera?"keypress":"keydown"),function(e){
	e=e||event;
	if (!isLightboxOpen){

		var scheduler = window.scheduler;
		
		if (e.keyCode == 37 || e.keyCode == 39) { // Left, Right arrows
			e.cancelBubble = true;
			
		    var next = scheduler.date.add(scheduler._date,(e.keyCode == 37 ? -1 : 1 ),scheduler._mode);
		    scheduler.setCurrentView(next);
		    return true;
		}

		var select_id = scheduler._select_id;
		if (e.ctrlKey && e.keyCode == 67) {  // CTRL+C
			if (select_id) {
				scheduler._buffer_id = select_id;
				isCopy = true;
				scheduler.callEvent("onEventCopied", [select_id, scheduler.getEvent(select_id)]);
			}
			return true;
		}
		if (e.ctrlKey && e.keyCode == 88) { // CTRL+X
			if (select_id) {
				isCopy = false;
				scheduler._buffer_id = select_id;
				var ev = scheduler.getEvent(select_id);
				scheduler.updateEvent(ev.id);
				scheduler.callEvent("onEventCut", [select_id, ev]);
			}
		}

		if (e.ctrlKey && e.keyCode == 86) {  // CTRL+V
			var ev = scheduler.getEvent(scheduler._buffer_id);
			if (ev) {
				var event_duration = ev.end_date-ev.start_date;
				if (isCopy) {
					var new_ev = scheduler._lame_clone(ev);
					new_ev.id = scheduler.uid();
					new_ev.start_date = new Date(date);
					new_ev.end_date = new Date(new_ev.start_date.valueOf() + event_duration);
					scheduler.addEvent(new_ev);
					scheduler.callEvent("onEventPasted", [isCopy, new_ev.id, new_ev, scheduler._buffer_id, ev]);
				}
				else { // cut operation
					ev.start_date = new Date(date);
					ev.end_date = new Date(ev.start_date.valueOf() + event_duration);
					scheduler.updateEvent(ev.id);
					scheduler.callEvent("onEventPasted", [isCopy, ev.id, ev]);
					isCopy = true; // switch to copy after first paste operation
				}
			}
			return true;
		}
	}
});

})();