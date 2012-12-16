/*
This software is allowed to use under GPL or you need to obtain Commercial or Enterise License
to use it in non-GPL project. Please contact sales@dhtmlx.com for details
*/
scheduler._props = {};
scheduler.createUnitsView = function(a, g, j, f, k, l) {
    if (typeof a == "object") j = a.list, g = a.property, f = a.size || 0, k = a.step || 1, l = a.skip_incorrect, a = a.name; scheduler._props[a] = { map_to: g, options: j, step: k, position: 0 }; if (f > scheduler._props[a].options.length) scheduler._props[a]._original_size = f, f = 0; scheduler._props[a].size = f; scheduler._props[a].skip_incorrect = l || !1; scheduler.date[a + "_start"] = scheduler.date.day_start; scheduler.templates[a + "_date"] = function(a) { return scheduler.templates.day_date(a) }; scheduler.templates[a +
"_scale_date"] = function(c) { var h = scheduler._props[a].options; if (!h.length) return ""; var g = (scheduler._props[a].position || 0) + Math.floor((scheduler._correct_shift(c.valueOf(), 1) - scheduler._min_date.valueOf()) / 864E5); return h[g].css ? "<span class='" + h[g].css + "'>" + h[g].label + "</span>" : h[g].label }; scheduler.date["add_" + a] = function(a, g) { return scheduler.date.add(a, g, "day") }; scheduler.date["get_" + a + "_end"] = function(c) {
    return scheduler.date.add(c, scheduler._props[a].size || scheduler._props[a].options.length,
"day")
}; scheduler.attachEvent("onOptionsLoad", function() { for (var c = scheduler._props[a], g = c.order = {}, f = c.options, i = 0; i < f.length; i++) g[f[i].key] = i; if (c._original_size && c.size == 0) c.size = c._original_size, delete c.original_size; c.size > f.length ? (c._original_size = c.size, c.size = 0) : c.size = c._original_size || c.size; scheduler._date && scheduler._mode == a && scheduler.setCurrentView(scheduler._date, scheduler._mode) }); scheduler.callEvent("onOptionsLoad", [])
};
scheduler.scrollUnit = function(a) { var g = scheduler._props[this._mode]; if (g) g.position = Math.min(Math.max(0, g.position + a), g.options.length - g.size), this.update_view() };
(function() {
    var a = function(b) { var d = scheduler._props[scheduler._mode]; if (d && d.order && d.skip_incorrect) { for (var a = [], e = 0; e < b.length; e++) typeof d.order[b[e][d.map_to]] != "undefined" && a.push(b[e]); b.splice(0, b.length); b.push.apply(b, a) } return b }, g = scheduler._pre_render_events_table; scheduler._pre_render_events_table = function(b, d) { b = a(b); return g.apply(this, [b, d]) }; var j = scheduler._pre_render_events_line; scheduler._pre_render_events_line = function(b, d) { b = a(b); return j.apply(this, [b, d]) }; var f = function(b,
d) { if (b && typeof b.order[d[b.map_to]] == "undefined") { var a = scheduler, e = 864E5, c = Math.floor((d.end_date - a._min_date) / e); d[b.map_to] = b.options[Math.min(c + b.position, b.options.length - 1)].key; return !0 } }, k = scheduler._reset_scale, l = scheduler.is_visible_events; scheduler.is_visible_events = function(b) { var d = l.apply(this, arguments); if (d) { var a = scheduler._props[this._mode]; if (a && a.size) { var e = a.order[b[a.map_to]]; if (e < a.position || e >= a.size + a.position) return !1 } } return d }; scheduler._reset_scale = function() {
    var b =
scheduler._props[this._mode], a = k.apply(this, arguments); if (b) {
        this._max_date = this.date.add(this._min_date, 1, "day"); for (var c = this._els.dhx_cal_data[0].childNodes, e = 0; e < c.length; e++) c[e].className = c[e].className.replace("_now", ""); if (b.size && b.size < b.options.length) {
            var g = this._els.dhx_cal_header[0], f = document.createElement("DIV"); if (b.position) f.className = "dhx_cal_prev_button", f.style.cssText = "left:1px;top:2px;position:absolute;", f.innerHTML = "&nbsp;", g.firstChild.appendChild(f), f.onclick = function() {
                scheduler.scrollUnit(b.step *
-1)
            }; if (b.position + b.size < b.options.length) f = document.createElement("DIV"), f.className = "dhx_cal_next_button", f.style.cssText = "left:auto; right:0px;top:2px;position:absolute;", f.innerHTML = "&nbsp;", g.lastChild.appendChild(f), f.onclick = function() { scheduler.scrollUnit(b.step) } 
        } 
    } return a
}; var c = scheduler._get_event_sday; scheduler._get_event_sday = function(b) { var a = scheduler._props[this._mode]; return a ? (f(a, b), a.order[b[a.map_to]] - a.position) : c.call(this, b) }; var h = scheduler.locate_holder_day; scheduler.locate_holder_day =
function(a, d, c) { var e = scheduler._props[this._mode]; return e && c ? (f(e, c), e.order[c[e.map_to]] * 1 + (d ? 1 : 0) - e.position) : h.apply(this, arguments) }; var m = scheduler._mouse_coords; scheduler._mouse_coords = function() {
    var a = scheduler._props[this._mode], d = m.apply(this, arguments); if (a) {
        if (!this._drag_event) this._drag_event = {}; var c = this._drag_event; if (this._drag_id && this._drag_mode) c = this.getEvent(this._drag_id), this._drag_event._dhx_changed = !0; var e = Math.min(d.x + a.position, a.options.length - 1), f = a.map_to; d.section =
c[f] = a.options[e].key; d.x = 0
    } return d
}; var i = scheduler._time_order; scheduler._time_order = function(a) { var d = scheduler._props[this._mode]; d ? a.sort(function(a, b) { return d.order[a[d.map_to]] > d.order[b[d.map_to]] ? 1 : -1 }) : i.apply(this, arguments) }; scheduler.attachEvent("onEventAdded", function(a, d) { if (this._loading) return !0; for (var c in scheduler._props) { var e = scheduler._props[c]; if (typeof d[e.map_to] == "undefined") d[e.map_to] = e.options[0].key } return !0 }); scheduler.attachEvent("onEventCreated", function(a, c) {
    var g =
scheduler._props[this._mode]; if (g && c) { var e = this.getEvent(a); this._mouse_coords(c); f(g, e); this.event_updated(e) } return !0
})
})();