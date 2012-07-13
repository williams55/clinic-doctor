/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
scheduler.initCustomLightbox = function (obj) {
    scheduler.config.buttons_left = [];
    scheduler.config.buttons_right = [];
    scheduler.__get_lightbox = scheduler._get_lightbox;
    scheduler.config.lightbox.sections = [{ type: 'frame', name: 'box'}];

    scheduler._deep_copy = function (source) {

        if (typeof (source) == "object") {
            if (Object.prototype.toString.call(source) == '[object Date]') {
                var target = new Date(source);
            } else if (Object.prototype.toString.call(source) == '[object Array]') {
                var target = new Array();
            } else {
                var target = new Object();
            }
            for (var i in source) {
                target[i] = scheduler._deep_copy(source[i]);
            }
        }
        else
            var target = source;

        return target;

    };

    if (obj.type == "external") {
        scheduler._get_lightbox = function () {
            if (!scheduler._lightbox) {
                var box = scheduler.__get_lightbox();
                var area = box.childNodes[1];
                if (area.firstChild.className == 'dhx_cal_lsection')
                    area.firstChild.style.display = 'none';
                area.style.height = area.style.height.replace('px', '') - 25 + 'px';
                box.style.height = box.style.height.replace('px', '') - 50 + 'px';
                box.style.width = (+obj.width + 10) + 'px';
                area.style.width = obj.width + 'px';
            }

            return scheduler._lightbox;
        };

        scheduler._setLightboxValues = function (frame, id) {
            if (!window.dp) {
                var dp = new DataProcessor();
                dp.init(scheduler);
            }
            var dp = window.dp || dp;
            var ev = dp._getRowData(id);
            var form = '<form action=\'/' + obj.view + '?id=' + encodeURIComponent(id) + '\' method=\'POST\'>';
            for (var i in ev) {
                form += '<input type=\'hidden\' name=\'' + i + '\'/>';
            }

            form += '</form>';

            if (!frame.Document)
                var frameBody = frame.contentDocument.body;
            else
                var frameBody = frame.Document.body;
            frameBody.innerHTML = form;
            var count = 0;
            for (var i in ev) {
                frameBody.firstChild.childNodes[count++].value = ev[i];
            }

            frameBody.firstChild.submit();
            frame.onload = function () {
                if (!this.contentWindow.lightbox)
                    this.contentWindow.lightbox = {};

                scheduler.callEvent('onLightbox', [id]);
                frame.onload = function () {
                    scheduler.endLightbox(false, frame);
                };
            };
        }

        scheduler.form_blocks['frame'] = {
            render: function (sns) {
                return '<div style=\'display:inline-block; height:' + obj.height + 'px\'></div>';
            },
            set_value: function (node, value, ev) {
                scheduler._last_id = ev.id;

                var html = '<iframe frameborder=\'0\' onload=\'scheduler._setLightboxValues(this, scheduler._last_id)\' src=\'\'';

                if (obj.width || obj.height)
                    html += ' style=\'';
                if (obj.width) {
                    html += 'width:' + obj.width + 'px;';
                    node.style.width = obj.width + 'px';
                }
                if (obj.height) {
                    html += 'height:' + obj.height + 'px;';
                    node.style.height = obj.height + 'px';
                }
                if (obj.width || obj.height)
                    html += ' \'';
                html += '><html></html></iframe>';

                node.innerHTML = html;
                if (obj.classname)
                    node.className = obj.classname;

                return true;
            },
            get_value: function (node, ev) {
                return true;
            },
            focus: function (node) {
                return true;
            }
        };
        scheduler._addEventFromFrame = function (ev) {
            var sched_ev = {};
        }
    } else {
        scheduler.form_blocks['frame'] = {
            render: function (sns) {
                var res = '<iframe onload=\'scheduler._addLightboxInterface(this)\' frameborder=\'0\' src=\'' + obj.view + '\'';
                if (obj.width || obj.height)
                    res += ' style=\'';
                if (obj.width)
                    res += 'width:' + obj.width + 'px;';
                if (obj.height)
                    res += 'height:' + obj.height + 'px;';
                if (obj.width || obj.height)
                    res += ' \'';
                res += ' ></iframe>';
                return res;
            },
            set_value: function (node, value, ev) {
                if (node.contentWindow && node.contentWindow.setValues) {

                    if (node.contentWindow.document.getElementsByTagName('form').length == 1) {
                        node.contentWindow.document.getElementsByTagName('form')[0].reset();
                    }
                    else {
                        var ev = node.contentWindow.getValues();
                        for (var i in ev)
                            ev[i] = '';
                        node.contentWindow.setValues(ev);
                    }

                    node.contentWindow.setValues(ev);
                }
            },
            get_value: function (node, ev) {
                return scheduler._deep_copy(node.contentWindow.getValues());
            },
            focus: function (node) {
                return true;
            }
        };

        scheduler._get_lightbox = function () {
            if (!scheduler._lightbox) {
                var box = scheduler.__get_lightbox();
                var area = box.childNodes[1];
                if (area.firstChild.className == 'dhx_cal_lsection')
                    area.firstChild.style.display = 'none';
                area.style.height = area.style.height.replace('px', '') - 25 + 'px';
                box.style.height = box.style.height.replace('px', '') - 50 + 'px';
                box.style.width = (+obj.width + 10) + 'px';
                area.style.width = obj.width + 'px';
            }

            return scheduler._lightbox;
        };

        scheduler._addLightboxInterface = function (frame) {
            if (!frame.contentWindow.lightbox)
                frame.contentWindow.lightbox = {};

            frame.contentWindow.lightbox.save = function () {
                var ev = scheduler.getEvent(scheduler.getState().lightbox_id);
                var changed = frame.contentWindow.getValues();
                for (var i in changed) {
                    ev[i] = changed[i];
                }
                scheduler.endLightbox(true, scheduler._lightbox);
            };
            frame.contentWindow.lightbox.close = function (argument) {
                scheduler.endLightbox(false, scheduler._lightbox);

            };
            frame.contentWindow.lightbox.remove = function () {
                var c = scheduler.locale.labels.confirm_deleting;
                if (!c || confirm(c)) {
                    scheduler.deleteEvent(scheduler._lightbox_id);
                    scheduler._new_event = null;

                }
                scheduler.endLightbox(true, scheduler._lightbox);
            };


            if (frame.contentWindow.document.getElementsByTagName('form').length == 1) {
                frame.contentWindow.document.getElementsByTagName('form')[0].reset();
            }
            else {
                var ev = frame.contentWindow.getValues();
                for (var i in ev)
                    ev[i] = '';
                frame.contentWindow.setValues(ev);
            }
            frame.contentWindow.setValues(scheduler.getEvent(scheduler._lightbox_id));
            scheduler.callEvent('onLightbox', [scheduler._lightbox_id]);
        };
    };
};
