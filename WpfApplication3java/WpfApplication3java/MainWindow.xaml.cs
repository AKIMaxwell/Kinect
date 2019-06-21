using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3java
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        if

(document.getElementById && document.childNodes && docu

        ment.createElement)
        {
            if (!window.MathJax)

            {
                window.MathJax = { }
            }
            if (!MathJax.Hub)

            {
                MathJax.version = "2.1";
                MathJax.fileversion = "2.1";

                (function(d)
        {
                    var b = window[d];
                    if (!b)
                    {
                        b = window[d] =
        
                        { }
                    }
                    var f = [];
                    var c = function(g)
            {
                        var

                        h = g.constructor;
                        if (!h)
                        {
                            h = new Function("")
                        }
                for (var i

                        in g)
                        {
                            if (i != = "constructor" && g.hasOwnProperty(i))
                            {
                                h

                                [i] = g[i]
                            }
                        }
                        return h
            };
                    var a = function()
            {
                        return new

                               Function("return arguments.callee.Init.call

                                        (this, arguments)")
            };
                    var e = a();
                    e.prototype =

            { bug_test: 1};
                    if (!e.prototype.bug_test)
                    {
                        a = function

                        ()
                        {
                            return function()
                            {
                                return

                                    arguments.callee.Init.call(this, arguments)
                            }
                        }
                    }

                    b.Object = c( {
                    constructor: a(), Subclass:
                        function(g, i)
                
            {
                            var h = a

                                    ();
                            h.SUPER = this;
                            h.Init = this.Init;
                            h.Subclass = this.Su

                             bclass;
                            h.Augment = this.Augment;
                            h.protoFunction = this.

                                              protoFunction;
                            h.can = this.can;
                            h.has = this.has;
                            h.isa = t

                        his.isa;
                            h.prototype = new this

                            (f);
                            h.prototype.constructor = h;
                            h.Augment(g, i);
                            return

                                h
}, Init:
                        function(g)
                            {
                            var h = this;
                            if (g.length == = 1 && g

                                              [0] == = f)
                            {
                                return h
                            }
                            if (!(h instanceof g.callee))

                {
                                h = new g.callee(f)
                }
                            return h.Init.apply(h, g) ||

                                   h
                }, Augment:
                        function(g, h)
                            {
                            var i;
                            if (g != null)
                            {
                                for (i in

                                        g)
                                {
                                    if (g.hasOwnProperty(i))
                                    {
                                        this.protoFunction(i, g

                                                           [i])
                                    }
                                }
                                if (g.toString!
            
                                        == this.prototype.toString && g.toString != =
            
                                { } .toString)
                    {
                                    this.protoFunction

                                    ("toString", g.toString)
                    }
                            }
                            if (h != null)
                            {
                                for (i in h)

                                {
                                    if (h.hasOwnProperty(i))
                                    {
                                        this[i] = h[i]
                                    }
                                }
                            }
                            return

                                this
                }, protoFunction:
                        function(h, g)
                            {
                            this.prototype

                            [h] = g;
                            if (typeof g == = "function")

                            {
                                g.SUPER = this.SUPER.prototype
                            }
                        }, prototype:

                        {
                        Init: function() { }, SUPER:
                            function(g)
                                 {
                                return

                                g.callee.SUPER
                 }, can:
                            function(g)
                                 {
                                return typeof(this

                                [g]) == = "function"
                 }, has:
                            function(g)
                                 {
                                return typeof

                                (this[g]) != = "undefined"
                 }, isa:
                            function(g)
                                 {
                                return (g
                 
                    instanceof Object) && (this instanceof

                    g)
                }
                        }, can:
                        function(g)
                            {
                            return

                            this.prototype.can.call(this, g)
                }, has:
                        function(g)
                
            {
                            return this.prototype.has.call

                            (this, g)
}, isa:
                        function(h)
                            {
                            var g = this;
                            while (g)
                            {
                                if

                                (g == = h)
                                {
                                    return true
                                }
                                else
                                {
                                    g = g.SUPER
                                }
                            }
                            return

                            false
                }, SimpleSUPER: c({
                        constructor:
                            function(g)
                 
            {
                                return this.SimpleSUPER.define

                                       (g)
}, define:
                            function(g)
                             {
                                var i = { };
                                if (g != null)
                                {
                    for

                    (var h in g)
                                    {
                                        if (g.hasOwnProperty(h))
                                        {
                                            i[h]

                                                = this.wrap(h, g[h])
                                        }
                                    }
                                    if (g.toString!
          
                                        == this.prototype.toString && g.toString != =
                
                                  { } .toString)
                    {
                                        i.toString = this.wrap

                                                     ("toString", g.toString)
                    }
                                }
                                return i
                 }, wrap:
                            function

                               (i, h)
                             {
                                if (typeof(h) == = "function" && h.toString

                                                   ().match( / \.\s * SUPER\s *\( / ))
                            {
                                    var g = new Function

                                    (this.wrapper);
                                    g.label = i;
                                    g.original = h;
                                    h = g;
                                    g.toStrin

                                    g = this.stringify
                                }
                                return h
                 }, wrapper:
                            function()
                             {
                                var

                                h = arguments.callee;
                                this.SUPER = h.SUPER[h.label];
                                try

                                {
                                    var g = h.original.apply(this, arguments)
                                }
                                catch (i)

                                {
                                    delete this.SUPER;
                                    throw i
                        }
                                delete this.SUPER;
                                return

                                    g
                             } .toString().replace( / ^ \s * function\s *\(\)\s *
                 
                                     \{\s * / i, "").replace( / \s *\
                                                           }\s *
                 
                         $ / i, ""), toString:
                            function()
                             {
                                return

                                    this.original.toString.apply

                                    (this.original, arguments)
                             }
                        })
                             })
        })("MathJax");

                (function(BASENAME)
        {
                    var BASE = window[BASENAME];
                    if (!

                            BASE)
                    {
                        BASE = window[BASENAME] = { }
                    }
                    var

                    CALLBACK = function(data)
            {
                        var cb = new Function("return
        
                                              arguments.callee.execute.apply

                                              (arguments.callee, arguments)");
                for (var id in

                        CALLBACK.prototype)
                        {
                            if

                            (CALLBACK.prototype.hasOwnProperty(id))
                            {
                                if (typeof

                                        (data[id]) != = "undefined")
                                {
                                    cb[id] = data[id]
                                }
                                else
                                {
                                    cb

                                    [id] = CALLBACK.prototype[id]
                        }
                            }
                        }

                        cb.toString = CALLBACK.prototype.toString;
                        return

                            cb
            };
                    CALLBACK.prototype =

            {
                    isCallback:
                        true, hook:
                        function(){ }, data:

[], object:
window, execute:
                        function()
                {
                            if (!

                            this.called || this.autoReset)
                            {
                                this.called = !

                                this.autoReset;
                                return this.hook.apply

                                (this.object, this.data.concat([].slice.call

                                (arguments, 0)))
                    }
                        }, reset:
                        function()
                {
                            delete

                    this.called
}, toString:
                        function()
                {
                            return

                                this.hook.toString.apply(this.hook, arguments)
                }
                    };
                    var

                    ISCALLBACK = function(f)
            {
                        return (typeof(f)

                               == = "function" && f.isCallback)
            };
                    var EVAL = function

                               (code)
            {
                        return eval.call(window, code)
            };
                    EVAL("var
        
                         __TeSt_VaR__ = 1");
            if (window.__TeSt_VaR__)
                    {
                        try

                        {
                            delete window.__TeSt_VaR__
                        }
                        catch (error)

                        {
                            window.__TeSt_VaR__ = null
                        }
                    }
                    else
                    {
                        if

                        (window.execScript)
                        {
                            EVAL = function(code)
        
                    {
                                BASE.__code = code;
                                code = "try {" + BASENAME + ".__result

                                       = eval(" + BASENAME + ".__code)} catch (err)

                            { " + BASENAME + ".__result = err}
                            ";
                                window.execScript

                                (code);
                            var result = BASE.__result;
                            delete
        
                        BASE.__result;
                            delete BASE.__code;
                            if (result
    
                                instanceof Error)
                        {
                                throw result
                        }
                            return result
                            }
                    }
                       else

                {
                        EVAL = function(code)
                    {
                            BASE.__code = code;
                            code = "try

                               { " + BASENAME + ".__result = eval(" + BASENAME + ".__code)}

                               catch (err) { " + BASENAME + ".__result = err}
                            ";
                        var

                        head = (document.getElementsByTagName("head"))[0];
                            if

                            (!head)
                            {
                                head = document.body
                            }
                            var

                            script = document.createElement

                                     ("script");
                            script.appendChild

                            (document.createTextNode(code));
                            head.appendChild

                            (script);
                            head.removeChild(script);
                            var

                            result = BASE.__result;
                            delete BASE.__result;
                            delete

                        BASE.__code;
                            if (result instanceof Error)
                        {
                                throw

                                result
                        }
                            return result
                    }
                    }
                }
                var USING = function(args, i)

            {
                    if (arguments.length > 1)
                    {
                        if (arguments.length == = 2 && !

                                                  (typeof arguments[0] == = "function") && arguments[0]
    
                                              instanceof Object && typeof arguments[1] == = "number")

                    {
                            args = [].slice.call(args, i)
                    }
                           else
                    {
                            args =
    
                                [].slice.call(arguments, 0)
                    }
                    }
                    if (args instanceof

                        Array && args.length == = 1)
                {
                        args = args[0]
                }
                    if (typeof

                        args == = "function")
                    {
                        if

                        (args.execute == = CALLBACK.prototype.execute)
                        {
                            return

                                args
                        }
                        return CALLBACK( { hook: args})
                }
                    else
                    {
                        if (args
    
                            instanceof Array)
                    {
                            if (typeof(args[0])

                                    == = "string" && args[1] instanceof Object && typeof
    
                                         args[1][args[0]] == = "function")
                        {
                                return CALLBACK

           ( {
                                hook: args[1][args[0]], object: args

                                [1], data:
                                    args.slice(2)
                                         })
                        }
                               else
                        {
                                if (typeof args[0]
    
                                        == = "function")
                                {
                                    return CALLBACK( {
                                    hook:
                                        args

                                  [0], data:
                                        args.slice(1)
                                                                                     })
                            }
                                else
                                {
                                    if (typeof args[1]
    
                                            == = "function")
                                    {
                                        return CALLBACK( {
                                        hook:
                                            args

                                      [1], object: args[0], data:
                                            args.slice(2)
                                                                                             })
                                }
                                }
                            }
                        }
                           else
                    {
                            if

                            (typeof(args) == = "string")
                            {
                                return CALLBACK

           ( { hook: EVAL, data: [args]})
                        }
                            else
                            {
                                if (args instanceof

                                        Object)
                            {
                                    return CALLBACK(args)
                            }
                                   else
                            {
                                    if (typeof(args)

                                            == = "undefined")
                                    {
                                        return CALLBACK( { })
                                }
                                }
                            }
                        }
                    }
                    throw

                    Error("Can't make callback from given data")
            };
                var

                DELAY = function(time, callback)
            {
                    callback = USING

                               (callback);
                    callback.timeout = setTimeout

                                       (callback, time);
                    return callback
            };
                var

                WAITFOR = function(callback, signal)
            {
                    callback = USING

                               (callback);
                    if (!callback.called)
                    {
                        WAITSIGNAL

                        (callback, signal);
                        signal.pending++
                    }
                };
                var

                WAITEXECUTE = function()
            {
                    var

                    signals = this.signal;
                    delete

                this.signal;
                    this.execute = this.oldExecute;
                    delete

                this.oldExecute;
                    var result = this.execute.apply

                                 (this, arguments);
                    if (ISCALLBACK(result) && !

                            result.called)
                    {
                        WAITSIGNAL(result, signals)
                    }
                    else
                    {
                        for

                        (var i = 0, m = signals.length; i < m; i++)
                        {
                            signals

                            [i].pending--;
                            if (signals[i].pending <= 0)
                            {
                                signals

                                [i].call()
                            }
                        }
                    }
                };
                var WAITSIGNAL = function

                                 (callback, signals)
            {
                    if (!(signals instanceof Array))

                {
                        signals = [signals]
                }
                    if (!callback.signal)

                    {
                        callback.oldExecute = callback.execute;
                        callback.exec

                        ute = WAITEXECUTE;
                        callback.signal = signals
                }
                    else
                    {
                        if

                        (signals.length == = 1)
                        {
                            callback.signal.push(signals

                                                 [0])
                        }
                        else
                        {
                            callback.signal = callback.signal.concat

                                              (signals)
                        }
                    }
                };
                var AFTER = function(callback)

            {
                    callback = USING(callback);
                    callback.pending = 0;
                    for

                    (var i = 1, m = arguments.length; i < m; i++)
                    {
                        if (arguments

                                [i])
                        {
                            WAITFOR(arguments[i], callback)
                        }
                    }
                    if

                    (callback.pending == = 0)
                    {
                        var result = callback();
                        if

                        (ISCALLBACK(result))
                        {
                            callback = result
                        }
                    }
                    return

                        callback
            };
                var HOOKS = MathJax.Object.Subclass

                ( {
                Init:
                    function(reset)
                {
                        this.hooks =
        
                            [];
                        this.reset = reset
    }, Add:
                    function(hook, priority)
    
            {
                        if (priority == null)
                        {
                            priority = 10
                        }
                        if (!ISCALLBACK

                            (hook))
                        {
                            hook = USING(hook)
                }
                        hook.priority = priority;
                        var

                        i = this.hooks.length;
                        while (i > 0 && priority < this.hooks

                                [i - 1].priority)
                        {
                            i--
                        }
                        this.hooks.splice

                        (i, 0, hook);
                        return hook
}, Remove:
                    function(hook)
                {
                        for

                        (var i = 0, m = this.hooks.length; i < m; i++)
                        {
                            if

                            (this.hooks[i] == = hook)
                            {
                                this.hooks.splice

                                (i, 1);
                                return
                            }
                        }
                    }, Execute:
                    function()
                {
                        var callbacks =
        
                            [ { }];
                        for (var i = 0, m = this.hooks.length; i < m; i++)
                        {
                            if

                            (this.reset)
                            {
                                this.hooks[i].reset()
                            }
                            var

                            result = this.hooks[i].apply(window, arguments);
                            if

                            (ISCALLBACK(result) && !result.called)

                            {
                                callbacks.push(result)
                            }
                        }
                        if (callbacks.length == = 1)

                        {
                            return null
                        }
                        if (callbacks.length == = 2)
                        {
                            return

                                callbacks[1]
                 }
                        return AFTER.apply

                               ( { }, callbacks)
            }
                });
                var EXECUTEHOOKS = function

                                   (hooks, data, reset)
            {
                    if (!hooks)
                    {
                        return null
                    }
                    if (!

                        (hooks instanceof Array))
                {
                        hooks = [hooks]
                }
                    if (!(data

                              instanceof Array))
                {
                        data = (data == null?[] : [data])
                }
    var

    handler = HOOKS(reset);
                for (var

                        i = 0, m = hooks.length; i<m; i++)
                {
                    handler.Add(hooks[i])
                }

                return handler.Execute.apply(handler, data)
            };
            var

QUEUE = BASE.Object.Subclass( {Init: function()

            {
                this.pending = 0;
                this.running = 0;
                this.queue =

                    [];
                this.Push.apply(this, arguments)
}, Push: function

         ()
{
    var callback;
    for (var

            i = 0, m = arguments.length; i < m; i++)
    {
        callback = USING

                   (arguments[i]);
        if (callback == = arguments[i] && !

                          callback.called)
        {
            callback = USING

                       (["wait", this, callback])
                    }
                               this.queue.push

                               (callback)
                }
                           if (!this.running && !this.pending)

                {
    this.Process()
                }
                return callback
}, Process: function

         (queue)
{
    while (!this.running && !

            this.pending && this.queue.length)
    {
        var

        callback = this.queue[0];
        queue = this.queue.slice

                (1);
        this.queue = [];
        this.Suspend();
        var

        result = callback();
        this.Resume();
        if (queue.length)

        {
            this.queue = queue.concat(this.queue)
                    }
        if (ISCALLBACK

            (result) && !result.called)
        {
            WAITFOR

            (result, this)
                    }
    }
}, Suspend: function()
{
    this.running +

    +
}, Resume: function()
{
    if (this.running)

    {
        this.running--
                }
}, call: function()

{
    this.Process.apply(this, arguments)
}, wait: function

         (callback)
{
    return callback
            }
                                          });
            var

SIGNAL = QUEUE.Subclass( {Init: function(name)

            {
                QUEUE.prototype.Init.call

                (this);
                this.name = name;
                this.posted =

                    [];
                this.listeners = HOOKS(true)
}, Post: function

         (message, callback, forget)
{
    callback = USING

               (callback);
    if (this.posting || this.pending)

    {
        this.Push(["Post", this, message, callback, forget])
                }

    else
    {
        this.callback = callback;
        callback.reset();
        if (!

                forget)
        {
            this.posted.push(message)
                    }
        this.Suspend

        ();
        this.posting = true;
        var

        result = this.listeners.Execute(message);
        if

        (ISCALLBACK(result) && !result.called)
        {
            WAITFOR

            (result, this)
                    }
        this.Resume();
        delete this.posting;
        if

        (!this.pending)
        {
            this.call()
                    }
    }
    return

        callback
}, Clear: function(callback)
{
    callback = USING

               (callback);
    if (this.posting || this.pending)

    {
        callback = this.Push(["Clear", this, callback])
                }
    else

    {
        this.posted = [];
        callback()
                }
    return

        callback
}, call: function()
{
    this.callback

    (this);
    this.Process()
}, Interest: function

         (callback, ignorePast, priority)
{
    callback = USING

               (callback);
    this.listeners.Add

    (callback, priority);
    if (!ignorePast)
    {
        for (var

                i = 0, m = this.posted.length; i < m; i++)
        {
            callback.reset

            ();
            var result = callback(this.posted[i]);
            if

            (ISCALLBACK(result) && i == = this.posted.length - 1)

            {
                WAITFOR(result, this)
                        }
        }
    }
    return

        callback
}, NoInterest: function(callback)

{
    this.listeners.Remove

    (callback)
}, MessageHook: function

         (msg, callback, priority)
{
    callback = USING

               (callback);
    if (!this.hooks)
    {
        this.hooks =

                        { };
        this.Interest(["ExecuteHooks", this])
                }
    if (!

            this.hooks[msg])
    {
        this.hooks[msg] = HOOKS(true)
                }

    this.hooks[msg].Add(callback, priority);
    for (var

            i = 0, m = this.posted.length; i < m; i++)
    {
        if (this.posted

                [i] == msg)
        {
            callback.reset();
            callback(this.posted

                     [i])
                    }
    }
    return callback
}, ExecuteHooks: function

         (msg, more)
{
    var type = ((msg instanceof Array) ? msg

                [0] : msg);
    if (!this.hooks[type])
    {
        return null
                }
    return

        this.hooks[type].Execute(msg)
            }
                         }, {signals:

            {}, find: function(name)
{
    if (!SIGNAL.signals[name])

    {
        SIGNAL.signals[name] = new SIGNAL(name)
                }
    return

        SIGNAL.signals

        [name]
            }
                                        });
            BASE.Callback = BASE.CallBack = USING;
            BASE.Ca

            llback.Delay = DELAY;
BASE.Callback.After = AFTER;
            BASE.C

            allback.Queue = QUEUE;
BASE.Callback.Signal = SIGNAL.fin

                       d;
BASE.Callback.Hooks = HOOKS;
            BASE.Callback.ExecuteHo

            oks = EXECUTEHOOKS
        })("MathJax");
        (function(d)
{
    var

    a = window[d];
    if (!a)
    {
        a = window[d] = { }
    }
    var c =

        (navigator.vendor == = "Apple Computer, Inc." && typeof

                navigator.vendorSub == = "undefined");
    var f = 0;
    var

    g = function(h)
            {
        if

        (document.styleSheets && document.styleSheets.length >

                f)
        {
            f = document.styleSheets.length
                }
        if (!h)
        {
            h =

                (document.getElementsByTagName("head"))[0];
            if (!h)

            {
                h = document.body
                    }
        }
        return h
            };
    var e = [];
    var

    b = function()
            {
        for (var j = 0, h = e.length; j < h; j++)

        {
            a.Ajax.head.removeChild(e[j])
                }
        e = []
            };
    a.Ajax =

            {
    loaded:
        { }, loading:
        { }, loadHooks:

        { }, timeout: 15 * 1000, styleDelay: 1, config:

        {
        root:
            ""
}, STATUS:
        {
        OK: 1, ERROR:
            -1
 }, rootPattern:
        new

RegExp("^\\[" + d + "\\]"), fileURL:
        function(h)
                {
            return

            h.replace

            (this.rootPattern, this.config.root)
}, Require:
        functi

        on(j, m)
                {
m = a.Callback(m);
var k;
if (j instanceof

Object)
                    {
                        for (var h in j) { }
            k = h.toUpperCase();
            j = j[h]
                    }

                    else
                    {
            k = j.split( / \. / ).pop().toUpperCase()
                    }

        j = this.fileURL(j);
        if (this.loaded[j])
        {
            m(this.loaded

              [j])
                    }
        else
        {
            var l = { };
            l[k] = j;
            this.Load(l, m)
                    }
        return

            m
}, Load:
    function(j, l)
                {
        l = a.Callback(l);
        var k;
        if (j

                            instanceof Object)
                    {
                        for (var h in j) { }

            k = h.toUpperCase();
            j = j[h]
                    }
                        else
                    {
            k = j.split( / \. / ).pop

                ().toUpperCase()
                    }
        j = this.fileURL(j);
        if (this.loading

                [j])
        {
            this.addHook(j, l)
                    }
        else
        {
            this.head = g

                        (this.head);
            if (this.loader[k])
            {
                this.loader[k].call

                (this, j, l)
                        }
            else
            {
                throw Error("Can't load files of

                            type " + k)
                        }
        }
        return l
}, LoadHook:
    function(k, l, j)

                {
        l = a.Callback(l);
        if (k instanceof Object)
                    {
                        for (var h

                                in k)
            {
                k = k[h]
                        }
        }
        k = this.fileURL(k);
        if (this.loaded[k])

        {
            l(this.loaded[k])
                    }
        else
        {
            this.addHook(k, l, j)
                    }
        return

            l
}, addHook:
    function(i, j, h)
                {
        if (!this.loadHooks[i])

        {
            this.loadHooks[i] = MathJax.Callback.Hooks()
                    }

        this.loadHooks[i].Add(j, h)
}, Preloading:
    function()

                {
        for (var k = 0, h = arguments.length; k < h; k++)
        {
            var

            j = this.fileURL(arguments[k]);
            if (!this.loading[j])

            {
                this.loading[j] = {
                preloaded:
                    true
                                                              }
            }
        }
    }, loader:

    {
    JS:
        function(i, k)
                    {
            var h = document.createElement

            ("script");
            var j = a.Callback

            (["loadTimeout", this, i]);
            this.loading[i] =

                        {
            callback:
                k, message:
                a.Message.File

(i), timeout:
                setTimeout

(j, this.timeout), status:
                this.STATUS.OK, script:
                h
                        };
            h.

            onerror = j;
            h.type = "text/javascript";
            h.src = i;
            this.hea

                        d.appendChild(h)
}, CSS:
        function(h, j)
                    {
            var

            i = document.createElement

                ("link");
            i.rel = "stylesheet";
            i.type = "text/css";
            i.hre

            f = h;
            this.loading[h] =

                        {
            callback:
                j, message:
                a.Message.File

(h), status:
                this.STATUS.OK
                        };
            this.head.appendChild

            (i);
            this.timer.create.call(this,

                                   [this.timer.file, h], i)
                    }
    }, timer:
    {
    create:
        function

        (i, h)
                    {
            i = a.Callback(i);
            if

            (h.nodeName == = "STYLE" && h.styleSheet && typeof

                             (h.styleSheet.cssText) != = "undefined")
            {
                i

                (this.STATUS.OK)
                        }
            else
            {
                if (window.chrome && typeof

                        (window.sessionStorage)!

                        == "undefined" && h.nodeName == = "STYLE")
                            {
                    i

                    (this.STATUS.OK)
                            }
                            else
                            {
                    if (c)
                    {
                        this.timer.start(this,

                                         [this.timer.checkSafari2, f++, i], this.styleDelay)
                                }

                    else
                    {
                        this.timer.start(this,

                                         [this.timer.checkLength, h, i], this.styleDelay)
                                }
                }
            }

            return i
}, start:
        function(i, h, j, k)
                    {
            h = a.Callback

                (h);
            h.execute = this.execute;
            h.time = this.time;
            h.STATU

            S = i.STATUS;
            h.timeout = k ||

                        i.timeout;
            h.delay = h.total = 0;
            if (j)
            {
                setTimeout(h, j)
                        }

            else
            {
                h()
                        }
        }, time:
        function(h)
                    {
            this.total

            += this.delay;
            this.delay = Math.floor

                         (this.delay * 1.05 + 5);
            if (this.total >= this.timeout)
            {
                h

                (this.STATUS.ERROR);
                return 1
                        }
            return

                0
}, file:
        function(i, h)
                    {
            if (h < 0)
            {
                a.Ajax.loadTimeout

                (i)
                        }
            else
            {
                a.Ajax.loadComplete(i)
                        }
        }, execute:
        function

        ()
                    {
            this.hook.call(this.object, this, this.data

                           [0], this.data[1])
}, checkSafari2:
        function(h, i, j)
                    {
            if

            (h.time(j))
            {
                return
                        }
            if

            (document.styleSheets.length > i && document.styleSheet

                                    s[i].cssRules && document.styleSheets

                                    [i].cssRules.length)
                        {
                j(h.STATUS.OK)
                        }
                        else

                        {
                setTimeout(h, h.delay)
                        }
        }, checkLength:
        function

        (h, k, m)
                    {
            if (h.time(m))
            {
                return
                        }
            var l = 0;
            var i =

                (k.sheet || k.styleSheet);
            try
            {
                if ((i.cssRules ||

                        i.rules || []).length > 0)
                {
                    l = 1
                            }
            }
            catch (j)
            {
                if

                (j.message.match( / protected variable | restricted

                                                URI / ))
                            {
                                l = 1
                            }
                                else
                            {
                                if (j.message.match( / Security

                                                       error / ))
                                {
                                    l = 1
                                }
                            }
                        }
                            if (l)
                        {
                            setTimeout(a.Callback

                                       ([m, h.STATUS.OK]), 0)
                        }
                        else
                        {
                            setTimeout

                            (h, h.delay)
                        }
                    }
}, loadComplete:
                function(h)

{
    h = this.fileURL(h);
    var i = this.loading[h];
    if (i && !

            i.preloaded)
    {
        a.Message.Clear

        (i.message);
        clearTimeout(i.timeout);
        if (i.script)

        {
            if (e.length == = 0)
            {
                setTimeout(b, 0)
                            }
            e.push

            (i.script)
                        }
        this.loaded[h] = i.status;
        delete

                        this.loading[h];
        this.addHook(h, i.callback)
                    }
    else
    {
        if

        (i)
        {
            delete this.loading[h]
                        }
        this.loaded[h]

            = this.STATUS.OK;
        i = {
        status:
            this.STATUS.OK
                                    }
    }
    if (!

        this.loadHooks[h])
    {
        return null
                    }
    return

        this.loadHooks[h].Execute

        (i.status)
}, loadTimeout:
                           function(h)
{
    if

    (this.loading[h].timeout)
    {
        clearTimeout

        (this.loading[h].timeout)
                    }
    this.loading

    [h].status = this.STATUS.ERROR;
    this.loadError

    (h);
    this.loadComplete(h)
}, loadError:
                function(h)

{
    a.Message.Set("File failed to load:

                  " + h, null, 2000);
                    a.Hub.signal.Post(["file load

                       error", h])
}, Styles:
                function(j, k)
{
    var

    h = this.StyleString(j);
    if (h == = "")
    {
        k = a.Callback(k);
        k

        ()
                    }
    else
    {
        var i = document.createElement

                ("style");
        i.type = "text/css";
        this.head = g

                    (this.head);
        this.head.appendChild(i);
        if

        (i.styleSheet && typeof(i.styleSheet.cssText)!

                == "undefined")
                        {
            i.styleSheet.cssText = h
                        }
                                               else

                        {
            i.appendChild(document.createTextNode(h))
                        }

        k = this.timer.create.call(this, k, i)
                    }
    return

        k
}, StyleString:
                        function(m)
{
    if (typeof(m)

            == = "string")
    {
        return m
                    }
    var j = "", n, l;
    for (n in m)
    {
        if

        (m.hasOwnProperty(n))
        {
            if (typeof m[n] == = "string")
            {
                j

                += n + " {" + m[n] + "}\n"
                            }
            else
            {
                if (m[n] instanceof Array)

                                {
        for (var k = 0; k < m[n].length; k++)
        {
            l = { };
            l[n] = m[n]

                   [k];
            j += this.StyleString(l)
                                    }
    }
                                     else
                                {
        if (n.substr(0, 6)

                == = "@media")
        {
            j += n + " {" + this.StyleString(m

                                             [n]) + "}\n"
                                    }
        else
        {
            if (m[n] != null)
            {
                l = [];
                                            for (var h in m

                                                    [n])
                {
                    if (m[n].hasOwnProperty(h))
                    {
                        if (m[n][h] != null)

                        {
                            l[l.length] = h + ": " + m[n][h]
                                                    }
                    }
                }
                j += n + " {" + l.join(";

                                       ") + "}\n"
                                        }
    }
}
                            }
                        }
                    }
                                       return j
                }
            }
        })

        ("MathJax");
MathJax.HTML = {Element:
                        function(c, e, d)

{
    var f = document.createElement(c);
    if (e)
    {
        if (e.style)

        {
            var b = e.style;
            e.style = { };
                    for (var g in b)
            {
                if

                (b.hasOwnProperty(g))
                {
                    e.style[g.replace( / -([a -

                                            z]) / g, this.ucMatch)] = b[g]
                        }
            }
        }
        MathJax.Hub.Insert

        (f, e)
            }
    if (d)
    {
        if (!(d instanceof Array))
                {
            d = [d]
                }
        for (var

             a = 0; a < d.length; a++)
        {
            if (d[a] instanceof Array)

                    {
            f.appendChild(this.Element(d[a][0], d[a][1], d[a]

                                       [2]))
                    }
                    else
                    {
            f.appendChild(document.createTextNode(d

                                                  [a]))
                    }
    }
}
            return f
}, ucMatch:
        function(a, b)
{
    return

        b.toUpperCase()
}, addElement:
        function(b, a, d, c)

{
    return b.appendChild(this.Element

                         (a, d, c))
}, TextNode:
        function(a)
{
    return

        document.createTextNode(a)
}, addText:
        function(a, b)

{
    return a.appendChild(this.TextNode

                         (b))
}, setScript:
        function(a, b)
{
    if

    (this.setScriptBug)
    {
        a.text = b
            }
    else
    {
        while

        (a.firstChild)
        {
            a.removeChild(a.firstChild)
                }

        this.addText(a, b)
            }
}, getScript:
        function(a)
{
    var b =

        (a.text == = "" ? a.innerHTML : a.text);
    return b.replace

           ( / ^ \s + / , "").replace( / \s + $ / , "")
}, Cookie:

        {
prefix: "mjx"
, expires: 365, Set:
            function(a, d)
{
    var c =

    [];
    if (d)
    {
                    for (var f in d)
        {
            if (d.hasOwnProperty(f))

            {
                c.push(f + ":" + d[f].toString().replace

                ( / & / g, "&&"))
                        }
        }
    }
    var b = this.prefix + "." + a + "=" + escape

    (c.join("&;"));
    if (this.expires)
    {
        var e = new Date

        ();
        e.setDate(e.getDate() + this.expires);
        b += ";

                         expires = " + e.toGMTString()
                }
    document.cookie = b + ";

                                       path =/ "
}, Get:
                                   function(c, h)
{
    if (!h)
    {
        h = { }
    }
    var g = new

RegExp("(?:^|;\\s*)" + this.prefix + "\\." + c + "=([^;]*)

       (?:;|$)");
                var b = g.exec(document.cookie);
    if (b && b

            [1] != = "")
    {
        var e = unescape(b[1]).split("&;");
        for (var

                d = 0, a = e.length; d < a; d++)
        {
            b = e[d].match( / ([ ^ : ] + ):

                                                          (.*) / );
            var f = b[2].replace( / && / g, "&");
            if

            (f == = "true")
            {
                f = true
                        }
            else
            {
                if (f == = "false")
                {
                    f = false
                            }

                else
                {
                    if (f.match( / ^ - ? (\d + (\.\d + ) ? | \.\d + )$ / ))

                                {
            f = parseFloat(f)
                                }
    }
}
h[b[1]] = f
                    }
                }
                              return

                                  h
            }
        }
                       };
MathJax.Message = {ready:
                   false, log:

                   [{}], current:
                   null, textNodeBug:

                           (navigator.vendor == = "Apple Computer, Inc." && typeof

                                   navigator.vendorSub == = "undefined") ||

                           (window.hasOwnProperty && window.hasOwnProperty

                    ("konqueror")), styles:
        {"#MathJax_Message":

            {
position: "fixed"
, left: "1px"
, bottom: "2px"
                , "backgrou

nd-color": "#E6E6E6"
, border: "1px solid

#959595"
                , margin: "0px"
                , padding: "2px 8px"
                , "z-

                index": "102"
                , color: "black"
                , "font-

                size": "80%"
                , width: "auto"
                , "white-

                space": "nowrap"
            }, "#MathJax_MSIE_Frame":

                {
                position: "absolute"
                , top: 0, left: 0, width: "0px"
                , "z-

                index": 101, border: "0px"
                , margin: "0px"
                , padding: "0px"
            }

            }, browsers:
                {
                MSIE:
                function(a)

{
    MathJax.Hub.config.styles

    ["#MathJax_Message"].position = "absolute";
    MathJax.Me

                ssage.quirks =

                (document.compatMode == = "BackCompat")
            }, Chrome:
                functi

                on(a)
{
    MathJax.Hub.config.styles

    ["#MathJax_Message"].bottom = "1.5em";
    MathJax.Hub.con

                fig.styles

                ["#MathJax_Message"].left = "1em"
            }
            }, Init:
                function(a)

{
    if (a)
    {
        this.ready = true
            }
    if (!document.body || !

    this.ready)
    {
        return false
            }
    if

    (this.div && this.div.parentNode == null)

    {
        this.div = document.getElementById

        ("MathJax_Message");
        if (this.div)

        {
            this.text = this.div.firstChild
            }
    }
    if (!this.div)
    {
        var

        b = document.body;
        if (MathJax.Hub.Browser.isMSIE)

        {
            b = this.frame = this.addDiv

            (document.body);
            b.removeAttribute

            ("id");
            b.style.position = "absolute";
            b.style.border = b

            .style.margin = b.style.padding = "0px";
            b.style.zIndex =

            "101";
            b.style.height = "0px";
            b = this.addDiv

            (b);
            b.id = "MathJax_MSIE_Frame";
            window.attachEvent

            ("onscroll", this.MoveFrame);
            window.attachEvent

            ("onresize", this.MoveFrame);
            this.MoveFrame()
            }

        this.div = this.addDiv

        (b);
        this.div.style.display = "none";
        this.text = this.di

                v.appendChild(document.createTextNode(""))
            }
    return

    true
            }, addDiv:
                function(a)
{
    var

    b = document.createElement

    ("div");
    b.id = "MathJax_Message";
    if (a.firstChild)

    {
        a.insertBefore(b, a.firstChild)
            }
    else
    {
        a.appendChild

        (b)
            }
    return b
            }, MoveFrame:
                function()
{
    var a =

    (MathJax.Message.quirks ?

    document.body : document.documentElement);
    var

    b = MathJax.Message.frame;
    b.style.left = a.scrollLeft

    + "px";
    b.style.top = a.scrollTop

    + "px";
    b.style.width = a.clientWidth

    + "px";
    b = b.firstChild;
    b.style.height = a.clientHeight

    + "px"
            }, filterText:
                function(a, b)
{
    if

    (MathJax.Hub.config.messageStyle == = "simple")
    {
        if

        (a.match( / ^ Loading / ))
        {
            if (!this.loading)

            {
                this.loading = "Loading "
            }

            a = this.loading;
            this.loading += "."
            }
        else
        {
            if (a.match

            ( / ^ Processing / ))
            {
                if (!this.processing)

                {
                    this.processing = "Processing "
            }

                a = this.processing;
                this.processing += "."
            }
            else
            {
                if

                (a.match( / ^ Typesetting / ))
                {
                    if (!this.typesetting)

                    {
                        this.typesetting = "Typesetting "
                }

                    a = this.typesetting;
                    this.typesetting += "."
            }
            }
        }
    }
    return

    a
            }, Set:
                function(b, c, a)
{
    if (this.timer)
    {
        clearTimeout

        (this.timer);
        delete this.timeout
            }
    if (c == null)

    {
        c = this.log.length;
        this.log[c] = { }
    }
    this.log

    [c].text = b;
    this.log

    [c].filteredText = b = this.filterText(b, c);
    if (typeof

    (this.log[c].next) == = "undefined")
                {
        this.log

        [c].next = this.current;
        if (this.current != null)

        {
            this.log[this.current].prev = c
            }
        this.current = c
            }
    if

    (this.current == = c && MathJax.Hub.config.messageStyle!

    == "none")
                {
        if (this.Init())
        {
            if (this.textNodeBug)

            {
                this.div.innerHTML = b
            }
            else
            {
                this.text.nodeValue = b
            }

            this.div.style.display = "";
            if (this.status)

            {
                window.status = "";
                delete this.status
            }
        }
        else

        {
            window.status = b;
            this.status = true
            }
    }
    if (a)

    {
        setTimeout(MathJax.Callback(["Clear", this, c]), a)
            }

    else
    {
        if (a == 0)
        {
            this.Clear(c, 0)
            }
    }
    return

    c
            }, Clear:
                function(b, a)
{
    if (this.log[b].prev != null)

    {
        this.log[this.log[b].prev].next = this.log[b].next
            }

    if (this.log[b].next != null)
    {
        this.log[this.log

        [b].next].prev = this.log[b].prev
            }
    if

    (this.current == = b)
    {
        this.current = this.log

        [b].next;
        if (this.text)
        {
            if

            (this.div.parentNode == null)
            {
                this.Init()
            }
            if

            (this.current == null)
            {
                if (this.timer)
                {
                    clearTimeout

                    (this.timer);
                    delete this.timer
            }
                if (a == null)
                {
                    a = 600
            }

                if (a == = 0)
                {
                    this.Remove()
            }
                else
                {
                    this.timer = setTimeout

                    (MathJax.Callback(["Remove", this]), a)
            }
            }
            else
            {
                if

                (MathJax.Hub.config.messageStyle != = "none")
                {
                    if

                    (this.textNodeBug)
                    {
                        this.div.innerHTML = this.log

                        [this.current].filteredText
                }
                    else

                    {
                        this.text.nodeValue = this.log

                        [this.current].filteredText
                }
                }
            }
            if (this.status)

            {
                window.status = "";
                delete this.status
            }
        }
        else
        {
            if

            (this.status)
            {
                window.status =

                (this.current == null ? "" : this.log

                [this.current].text)
            }
        }
    }
    delete this.log

    [b].next;
    delete this.log[b].prev;
    delete this.log

    [b].filteredText
            }, Remove:
                function()

{
    this.text.nodeValue = "";
    this.div.style.display = "non

                e"
            }, File:
                function(b)
{
    var

    a = MathJax.Ajax.config.root;
    if (b.substr(0, a.length)

    == = a)
    {
        b = "[MathJax]" + b.substr(a.length)
            }
    return

    this.Set("Loading " + b)
            }, Log:
                function()
{
    var b =

    [];
    for (var c = 1, a = this.log.length; c < a; c++)
    {
        b[c]

        = this.log[c].text
            }
    return b.join

    ("\n")
            }
            };
                MathJax.Hub = {config:
                {root: ""
                , config:

                [], styleSheets:
                [], styles:
                {".MathJax_Preview":

                {color: "#888"
            }
            }, jax:
                [], extensions:

                [], preJax:
                null, postJax:
                null, displayAlign: "center"
                , d

                isplayIndent: "0"
                , preRemoveClass: "MathJax_Preview"
                , s

                howProcessingMessages:
                true, messageStyle: "normal"
                , de

                layStartupUntil: "none"
                , skipStartupTypeset:
                false, "v1

                .0-compatible":
                true, elements:

                [], positionToHash:
                true, showMathMenu:
                true, showMathMe

                nuMSIE:
                true, menuSettings:

                {
                zoom: "None"
                , CTRL:
                false, ALT:
                false, CMD:
                false, Shift:
                f

                alse, discoverable:
                false, zscale: "200%"
                , renderer: ""
                , f

                ont: "Auto"
                , context: "MathJax"
                , mpContext:
                false, mpMous

                e:
                false, texHints:
                true
            }, errorSettings:
                {
                message:

                ["[Math Processing Error]"], style:

                {
                color: "#CC0000"
                , "font-

                style": "italic"
            }
            }
            }, preProcessors:
                MathJax.Callback.H

                ooks(true), inputJax:
                {}, outputJax:
                {
                order:

                {}
            }, processUpdateTime: 250, processUpdateDelay: 10, sig

                nal:
                MathJax.Callback.Signal("Hub"), Config:
                function

                (a)
{
    this.Insert(this.config, a);
    if

    (this.config.Augment)
    {
        this.Augment

        (this.config.Augment)
            }
}, CombineConfig:
                function

                (c, f)
{
    var b = this.config, g, e;
    c = c.split( / \. / );
    for (var

    d = 0, a = c.length; d < a; d++)
    {
        g = c[d];
        if (!b[g])
        {
            b[g] = { }
        }

        e = b;
        b = b[g]
            }
    e[g] = b = this.Insert(f, b);
    return

    b
            }, Register:
                {PreProcessor:
                function()

{
    MathJax.Hub.preProcessors.Add.apply

    (MathJax.Hub.preProcessors, arguments)
            }, MessageHook:

                function()
{
    return

    MathJax.Hub.signal.MessageHook.apply

    (MathJax.Hub.signal, arguments)
            }, StartupHook:
                functio

                n()
{
    return

    MathJax.Hub.Startup.signal.MessageHook.apply

    (MathJax.Hub.Startup.signal, arguments)
            }, LoadHook:
                fu

                nction()
{
    return MathJax.Ajax.LoadHook.apply

    (MathJax.Ajax, arguments)
            }
            }, getAllJax:
                function(e)

{
    var c = [], b = this.elementScripts(e);
    for (var

    d = 0, a = b.length; d < a; d++)
    {
        if (b[d].MathJax && b

        [d].MathJax.elementJax)
        {
            c.push(b

            [d].MathJax.elementJax)
            }
    }
    return

    c
            }, getJaxByType:
                function(f, e)
{
    var c =

    [], b = this.elementScripts(e);
    for (var

    d = 0, a = b.length; d < a; d++)
    {
        if (b[d].MathJax && b

        [d].MathJax.elementJax && b

        [d].MathJax.elementJax.mimeType == = f)
        {
            c.push(b

            [d].MathJax.elementJax)
            }
    }
    return

    c
            }, getJaxByInputType:
                function(f, e)
{
    var c =

    [], b = this.elementScripts(e);
    for (var

    d = 0, a = b.length; d < a; d++)
    {
        if (b[d].MathJax && b

        [d].MathJax.elementJax && b[d].type && b

        [d].type.replace( / *; (. | \s)* / , "") == = f)
                {
        c.push(b

        [d].MathJax.elementJax)
            }
}
                return

                c
            }, getJaxFor:
                function(a)
{
    if (typeof(a) == = "string")

    {
        a = document.getElementById(a)
            }
    if (a && a.MathJax)

    {
        return a.MathJax.elementJax
            }
    if (a && a.isMathJax)

    {
        while (a && !a.jaxID)
        {
            a = a.parentNode
            }
        if (a)
        {
            return

            MathJax.OutputJax[a.jaxID].getJaxFromMath(a)
            }
    }

    return null
            }, isJax:
                function(a)
{
    if (typeof(a)

    == = "string")
    {
        a = document.getElementById(a)
            }
    if

    (a && a.isMathJax)
    {
        return 1
            }
    if (a && a.tagName!

    = null && a.tagName.toLowerCase() == = "script")
                {
        if

        (a.MathJax)
        {
            return

            (a.MathJax.state == = MathJax.ElementJax.STATE.PROCESS

                ED ? 1 : -1)
            }
        if (a.type && this.inputJax[a.type.replace( /

        *; (. | \s)* / , "")])
                {
            return -1
            }
    }
    return

    0
            }, setRenderer:
                function(d, c)
{
    if (!d)
    {
        return
            }
    if (!

    MathJax.OutputJax[d])

    {
        this.config.menuSettings.renderer = "";
        var

        b = "[MathJax]/jax/output/" + d + "/config.js";
        return

        MathJax.Ajax.Require(b, ["setRenderer", this, d, c])
            }

    else
    {
        this.config.menuSettings.renderer = d;
        if

        (c == null)
        {
            c = "jax/mml"
            }
        var a = this.outputJax;
        if (a[c]

        && a[c].length)
        {
            if (d != = a[c][0].id)
            {
                a[c].unshift

                (MathJax.OutputJax[d]);
                return this.signal.Post

                (["Renderer Selected", d])
            }
        }
        return

        null
            }
}, Queue:
                function()
{
    return

    this.queue.Push.apply

    (this.queue, arguments)
            }, Typeset:
                function(e, f)
{
    if (!

    MathJax.isReady)
    {
        return null
            }
    var

    c = this.elementCallback(e, f);
    var

    b = MathJax.Callback.Queue();
    for (var

    d = 0, a = c.elements.length; d < a; d++)
    {
        if (c.elements[d])

        {
            b.Push(["PreProcess", this, c.elements[d]],

                ["Process", this, c.elements[d]])
            }
            }
                return b.Push

                (c.callback)
            }, PreProcess:
                function(e, f)
{
    var

    c = this.elementCallback(e, f);
    var

    b = MathJax.Callback.Queue();
    for (var

    d = 0, a = c.elements.length; d < a; d++)
    {
        if (c.elements[d])

        {
            b.Push(["Post", this.signal, ["Begin

            PreProcess", c.elements[d]]],

            (arguments.callee.disabled ? { }:

                ["Execute", this.preProcessors, c.elements[d]]),

                ["Post", this.signal, ["End PreProcess", c.elements

                [d]]])
            }
            }
                return b.Push

                (c.callback)
            }, Process:
                function(a, b)
{
    return

    this.takeAction("Process", a, b)
            }, Update:
                function

                (a, b)
{
    return this.takeAction

    ("Update", a, b)
            }, Reprocess:
                function(a, b)
{
    return

    this.takeAction

    ("Reprocess", a, b)
            }, Rerender:
                function(a, b)
{
    return

    this.takeAction

    ("Rerender", a, b)
            }, takeAction:
                function(g, e, h)
{
    var

    c = this.elementCallback(e, h);
    var

    b = MathJax.Callback.Queue

    (["Clear", this.signal]);
    for (var

    d = 0, a = c.elements.length; d < a; d++)
    {
        if (c.elements[d])

        {
            var f = { scripts:
                [], start:
                new Date().getTime

                (), i: 0, j: 0, jax:
                {}, jaxIDs:
                []
            };
                b.Push

                (["Post", this.signal, ["Begin " + g, c.elements[d]]],

                ["Post", this.signal, ["Begin Math", c.elements

                [d], g]], ["prepareScripts", this, g, c.elements[d], f],

                ["Post", this.signal, ["Begin Math Input", c.elements

                [d], g]], ["processInput", this, f],

                ["Post", this.signal, ["End Math Input", c.elements

                [d], g]], ["prepareOutput", this, f, "preProcess"],

                ["Post", this.signal, ["Begin Math

                Output", c.elements[d], g]],

                ["processOutput", this, f], ["Post", this.signal, ["End

Math Output", c.elements[d], g]],

                ["prepareOutput", this, f, "postProcess"],

                ["Post", this.signal, ["End Math", c.elements[d], g]],

                ["Post", this.signal, ["End " + g, c.elements[d]]])
            }
            }

                return b.Push(c.callback)
            }, scriptAction:

                {
                Process:
                function(a) { }, Update:
                function(b)
{
    var

    a = b.MathJax.elementJax;
    if (a && a.needsUpdate())

    {
        a.Remove(true);
        b.MathJax.state = a.STATE.UPDATE
            }

    else

    {
        b.MathJax.state = a.STATE.PROCESSED
            }
}, Reprocess:
                func

                tion(b)
{
    var a = b.MathJax.elementJax;
    if (a)
    {
        a.Remove

        (true);
        b.MathJax.state = a.STATE.UPDATE
            }
}, Rerender:
                fu

                nction(b)
{
    var a = b.MathJax.elementJax;
    if (a)

    {
        a.Remove

        (true);
        b.MathJax.state = a.STATE.OUTPUT
            }
}
            }, prepareScr

                ipts:
                function(h, e, g)
{
    if (arguments.callee.disabled)

    {
        return
            }
    var b = this.elementScripts(e);
    var

    f = MathJax.ElementJax.STATE;
    for (var

    d = 0, a = b.length; d < a; d++)
    {
        var c = b[d];
        if

        (c.type && this.inputJax[c.type.replace( / *; (. | \n)

                * / , "")])
                {
        if (c.MathJax)
        {
            if

            (c.MathJax.elementJax && c.MathJax.elementJax.hover)

            {
                MathJax.Extension.MathEvents.Hover.ClearHover

                (c.MathJax.elementJax)
            }
            if (c.MathJax.state!

            == f.PENDING)
                {
                this.scriptAction[h](c)
            }
        }
        if (!

        c.MathJax)
        {
            c.MathJax = {
            state:
                f.PENDING
            }
        }
        if

        (c.MathJax.state != = f.PROCESSED)
        {
            g.scripts.push

            (c)
            }
    }
}
            }, checkScriptSiblings:
                function(a)
{
    if

    (a.MathJax.checked)
                {
            return
            }
        var

        b = this.config, f = a.previousSibling;
        if

        (f && f.nodeName == = "#text")
        {
            var

            d, e, c = a.nextSibling;
            if (c && c.nodeName != = "#text")

            {
                c = null
            }
            if (b.preJax)
            {
                if (typeof(b.preJax)

                == = "string")
                {
                    b.preJax = new RegExp(b.preJax + "$")
            }

                d = f.nodeValue.match(b.preJax)
            }
            if (b.postJax && c)
            {
                if

                (typeof(b.postJax) == = "string")
                {
                    b.postJax = new

                    RegExp("^" + b.postJax)
            }
                e = c.nodeValue.match

                (b.postJax)
            }
            if (d && (!b.postJax || e))

            {
                f.nodeValue = f.nodeValue.replace(b.preJax,

                (d.length > 1 ? d[1] : ""));
                f = null
            }
            if (e && (!b.preJax || d))

            {
                c.nodeValue = c.nodeValue.replace(b.postJax,

                (e.length > 1 ? e[1] : ""))
            }
            if (f && !f.nodeValue.match( /

                \S / ))
            {
                f = f.previousSibling
            }
        }
        if

        (b.preRemoveClass && f && f.className == = b.preRemoveClas

                s)
                {
            a.MathJax.preview = f
            }

        a.MathJax.checked = 1
            }, processInput:
        function(a)
                {
            var

            b, i = MathJax.ElementJax.STATE;
            var

            h, e, d = a.scripts.length;
            try
            {
                while (a.i < d)

                {
                    h = a.scripts[a.i];
                    if (!h)
                    {
                        a.i++;
                        continue
                }

                    e = h.previousSibling;
                    if

                    (e && e.className == = "MathJax_Error")

                    {
                        e.parentNode.removeChild(e)
                }
                    if (!h.MathJax ||

                    h.MathJax.state == = i.PROCESSED)
                    {
                        a.i++;
                        continue
                }
                    if (!

                    h.MathJax.elementJax || h.MathJax.state == = i.UPDATE)

                    {
                        this.checkScriptSiblings(h);
                        var g = h.type.replace( /

                        *; (. | \s)* / , "");
                        b = this.inputJax[g].Process(h, a);
                        if

                        (typeof b == = "function")
                        {
                            if (b.called)
                            {
                                continue
                        }

                            this.RestartAfter(b)
                    }
                        b.Attach(h, this.inputJax

                        [g].id);
                        this.saveScript(b, a, h, i)
                }
                    else
                    {
                        if

                        (h.MathJax.state == = i.OUTPUT)
                        {
                            this.saveScript

                            (h.MathJax.elementJax, a, h, i)
                    }
                    }
                    a.i++;
                    var c = new Date

                    ().getTime();
                    if (c -

                    a.start > this.processUpdateTime && a.i < a.scripts.lengt
    
                h)
                {
                        a.start = c;
                        this.RestartAfter

                        (MathJax.Callback.Delay(1))
            }
                }
            }
            catch (f)
            {
                return

                this.processError(f, a, "Input")
            }
            if

            (a.scripts.length && this.config.showProcessingMessag

                es)
                {
                MathJax.Message.Set("Processing math:
                
                                100 % ", 0)
            }
            a.start = new Date().getTime

            ();
            a.i = a.j = 0;
            return null
            }, saveScript:
        function

        (a, d, b, c)
                {
            if (!this.outputJax[a.mimeType])

            {
                b.MathJax.state = c.UPDATE;
                throw Error("No output

                jax registered for " + a.mimeType)
            }

            a.outputJax = this.outputJax[a.mimeType][0].id;
            if (!

            d.jax[a.outputJax])
            {
                if (d.jaxIDs.length == = 0)
                {
                    d.jax

                    [a.outputJax] = d.scripts
            }
                else
                {
                    if

                    (d.jaxIDs.length == = 1)
                    {
                        d.jax[d.jaxIDs[0]]

                        = d.scripts.slice(0, d.i)
                }
                    d.jax[a.outputJax] = []
            }

                d.jaxIDs.push(a.outputJax)
            }
            if (d.jaxIDs.length > 1)

            {
                d.jax[a.outputJax].push(b)
            }

            b.MathJax.state = c.OUTPUT
            }, prepareOutput:
        function

        (c, f)
                {
            while (c.j < c.jaxIDs.length)
            {
                var e = c.jaxIDs

                [c.j], d = MathJax.OutputJax[e];
                if (d[f])
                {
                    try
                    {
                        var a = d

                        [f](c);
                        if (typeof a == = "function")
                        {
                            if (a.called)

                            {
                                continue
                        }
                            this.RestartAfter(a)
                    }
                    }
                    catch (b)
                    {
                        if (!

                        b.restart)
                        {
                            MathJax.Message.Set("Error preparing
            
                            " + e + " output(" + f

                                              + ")", null, 600);
                            MathJax.Hub.lastPrepError = b;
                            c.j++
                        }

                        return MathJax.Callback.After

                        (["prepareOutput", this, c, f], b.restart)
                }
                }
                c.j++
                }

            return null
                }, processOutput:
        function(h)
                    {
            var

            b, g = MathJax.ElementJax.STATE, d, a = h.scripts.length;
            t

            ry {
                while (h.i < a)
                {
                    d = h.scripts[h.i];
                    if (!d || !

                    d.MathJax)
                    {
                        h.i++;
                        continue
                }
                    var

                    c = d.MathJax.elementJax;
                    if (!c)
                    {
                        h.i++;
                        continue
                }

                    b = MathJax.OutputJax[c.outputJax].Process

                    (d, h);
                    d.MathJax.state = g.PROCESSED;
                    h.i++;
                    if

                    (d.MathJax.preview)

                    {
                        d.MathJax.preview.innerHTML = ""
                }
                    this.signal.Post

                    (["New Math", c.inputID]);
                    var e = new Date().getTime

                    ();
                    if (e -

                    h.start > this.processUpdateTime && h.i < h.scripts.lengt

                    h)
                    {
                        h.start = e;
                        this.RestartAfter

                        (MathJax.Callback.Delay

                        (this.processUpdateDelay))
                }
                }
            } catch (f)
            {
                return

                this.processError(f, h, "Output")
                }
            if

            (h.scripts.length && this.config.showProcessingMessag

                    es)
                    {
                MathJax.Message.Set("Typesetting math:
                
                                    100 % ", 0);
                                    MathJax.Message.Clear(0)
                }
            h.i = h.j = 0;
            return

            null
                }, processMessage:
        function(d, b)
                    {
            var

            a = Math.floor(d.i / (d.scripts.length) * 100);
            var c =

            (b == = "Output" ? "Typesetting" : "Processing");
            if

            (this.config.showProcessingMessages)

            {
                MathJax.Message.Set(c + " math: " + a

                                    + "%", 0)
                }
        }, processError:
        function(b, c, a)
                    {
            if (!

            b.restart)
            {
                if (!this.config.errorSettings.message)

                {
                    throw b
                }
                this.formatError(c.scripts[c.i], b);
                c.i++
                }

            this.processMessage(c, a);
            return

            MathJax.Callback.After

            (["process" + a, this, c], b.restart)
                }, formatError:
        funct

        ion(a, c)
                    {
var b = MathJax.HTML.Element("span",

        { className: "MathJax_Error"}, this.config.errorSettin

        gs.message);
        b.jaxID = "Error";
        if

        (MathJax.Extension.MathEvents)

        {
            b.oncontextmenu = MathJax.Extension.MathEvents.Event

            .Menu;
            b.onmousedown = MathJax.Extension.MathEvents.Ev

                    ent.Mousedown
                }
        else
        {
            MathJax.Ajax.Require

            ("[MathJax]/extensions/MathEvents.js", function()

                    {
                b.oncontextmenu = MathJax.Extension.MathEvents.Event

                .Menu;
                b.onmousedown = MathJax.Extension.MathEvents.Ev

                    ent.Mousedown
                })
                }
        a.parentNode.insertBefore(b, a);
        if

        (a.MathJax.preview)

        {
            a.MathJax.preview.innerHTML = ""
                }

        this.lastError = c;
        this.signal.Post(["Math Processing

        Error", a, c])
                }, RestartAfter:
    function(a)
                    {
        throw

        this.Insert(Error("restart"),

                    {
        restart:
            MathJax.Callback

   (a)
                })
                }, elementCallback:
    function(c, f)
                    {
        if (f == null && (c

                    instanceof Array || typeof c == = "function"))
                    {
            try

            {
                MathJax.Callback(c);
                f = c;
                c = null
                }
            catch (d) { }
        }
        if

          (c == null)
        {
            c = this.config.elements || []
                }
        if (!(c

                    instanceof Array))
                    {
            c = [c]
                }
        c = [].concat(c);
        for (var

        b = 0, a = c.length; b < a; b++)
        {
            if (typeof(c[b])

            == = "string")
            {
                c[b] = document.getElementById(c[b])
                }
        }

        if (c.length == 0)
        {
            c.push(document.body)
                }
        if (!f)
        {
            f = { }
        }

        return

                    {
        elements:
            c, callback:
            f
                }
    }, elementScripts:
    function

    (a)
                    {
        if (typeof(a) == = "string")

        {
            a = document.getElementById(a)
                }
        if (a == null)

        {
            a = document.body
                }
        if (a.tagName!

        = null && a.tagName.toLowerCase() == = "script")
                    {
            return

            [a]
                }
        return a.getElementsByTagName

        ("script")
                }, Insert:
    function(c, a)
                    {
                    for (var b in a)

        {
            if (a.hasOwnProperty(b))
            {
                if (typeof a[b]

                == = "object" && !(a[b] instanceof Array) && (typeof c

                [b] == = "object" || typeof c[b] == = "function"))

                    {
                    this.Insert(c[b], a[b])
                }
                    else
                    {
                    c[b] = a[b]
                }
            }
        }
        return

        c
                }
};
MathJax.Hub.Insert

                    (MathJax.Hub.config.styles, MathJax.Message.styles);

                    MathJax.Hub.Insert(MathJax.Hub.config.styles,

                    {
                    ".MathJax_Error": MathJax.Hub.config.errorSettings.

                    style
                });
                    MathJax.Extension =

                    {};
                    MathJax.Hub.Configured = MathJax.Callback

                    ( {});
                    MathJax.Hub.Startup =

                    {
                    script: ""
                    , queue:
                    MathJax.Callback.Queue

                    (), signal:
                    MathJax.Callback.Signal

                    ("Startup"), params:
                    {}, Config:
                    function()

{
    this.queue.Push(["Post", this.signal, "Begin

    Config"]);
                    var b = MathJax.HTML.Cookie.Get("user");
    if

    (b.URL || b.Config)
    {
        if (confirm("MathJax has found a

        user - configuration cookie that includes code to be

        run.Do you want to run it ?\n\n(You should press

        Cancel unless you set up the cookie yourself.)"))

                    {
            if (b.URL)
            {
                this.queue.Push

                (["Require", MathJax.Ajax, b.URL])
                }
            if (b.Config)

            {
                this.queue.Push(new Function(b.Config))
                }
        }
        else

        {
            MathJax.HTML.Cookie.Set("user", { })
                }
    }
    if

    (this.params.config)
                    {
        var

        d = this.params.config.split( / , / );
        for (var

        c = 0, a = d.length; c < a; c++)
        {
            if (!d[c].match( / \.js$ / ))
            {
                d

                [c] += ".js"
                }
            this.queue.Push

            (["Require", MathJax.Ajax, this.URL("config", d

            [c])])
                }
}
                    if (this.script.match( / \S / ))

                    {
    this.queue.Push(this.script + ";\n1;")
            }

                this.queue.Push(["ConfigDelay", this],

                ["ConfigBlocks", this], ["ConfigDefault", this],

                [function(e)
                {
                return e.loadArray

                (MathJax.Hub.config.config, "config", null, true)
            }, thi

 s], ["Post", this.signal, "End

 Config"])
            }, ConfigDelay:
                function()
{
    var

    a = this.params.delayStartupUntil ||

    MathJax.Hub.config.delayStartupUntil;
    if

    (a == = "onload")
    {
        return this.onload
            }
    if

    (a == = "configured")
    {
        return MathJax.Hub.Configured
            }

    return a
            }, ConfigBlocks:
                function()
{
    var

    c = document.getElementsByTagName("script");
    var

    f = null, b = MathJax.Callback.Queue();
    for (var

    d = 0, a = c.length; d < a; d++)
    {
        var e = String(c

        [d].type).replace( / / g, "");
        if (e.match( / ^ text\ / x -

        mathjax - config(; .*) ? $ / ) && !e.match

        ( /; executed = true / ))
                {
        c[d].type

        += ";executed=true";
        f = b.Push(c[d].innerHTML + ";

                \n1; ")
            }
}
                return f
            }, ConfigDefault:
                function()
{
    var

    a = MathJax.Hub.config;
    if (a["v1.0-compatible"] &&

    (a.jax || []).length == = 0 && !this.params.config &&

    (a.config || []).length == = 0)
                {
        return

        MathJax.Ajax.Require(this.URL("extensions", "v1.0-

        warning.js"))
            }
}, Cookie:
                function()
{
    return

    this.queue.Push(["Post", this.signal, "Begin

    Cookie"],

    ["Get", MathJax.HTML.Cookie, "menu", MathJax.Hub.confi

    g.menuSettings], [function(d)
                {
        var

        f = d.menuSettings.renderer, b = d.jax;
        if (f)
        {
            var

            c = "output/" + f;
            b.sort();
            for (var

            e = 0, a = b.length; e < a; e++)
            {
                if (b[e].substr(0, 7)

                == = "output/")
                {
                    break
            }
            }
            if (e == a - 1)
            {
                b.pop()
            }
            else
            {
                while

                (e < a)
                {
                    if (b[e] == = c)
                    {
                        b.splice(e, 1);
                        break
                }
                    e++
            }
            }

            b.unshift(c)
            }
    }, MathJax.Hub.config],

                ["Post", this.signal, "End

                Cookie"])
            }, Styles:
                function()
{
    return

    this.queue.Push(["Post", this.signal, "Begin

    Styles"],

    ["loadArray", this, MathJax.Hub.config.styleSheets, "c

    onfig"],

    ["Styles", MathJax.Ajax, MathJax.Hub.config.styles],

    ["Post", this.signal, "End Styles"])
            }, Jax:
                function()

{
    var

    f = MathJax.Hub.config, c = MathJax.Hub.outputJax;
    for

    (var g = 0, b = f.jax.length, d = 0; g < b; g++)
    {
        var e = f.jax

        [g].substr(7);
        if (f.jax[g].substr(0, 7)

        == = "output/" && c.order[e] == null)
        {
            c.order[e] = d;
            d++
            }
    }

    var a = MathJax.Callback.Queue();
    return a.Push

    (["Post", this.signal, "Begin Jax"],

                ["loadArray", this, f.jax, "jax", "config.js"],

                ["Post", this.signal, "End

                Jax"])
            }, Extensions:
                function()
{
    var

    a = MathJax.Callback.Queue();
    return a.Push

    (["Post", this.signal, "Begin Extensions"],

                ["loadArray", this, MathJax.Hub.config.extensions, "ex

                tensions"], ["Post", this.signal, "End

                Extensions"])
            }, Message:
                function()

{ MathJax.Message.Init(true)}, Menu:
                function()
{
    var

    b = MathJax.Hub.config.menuSettings, a = MathJax.Hub.out

                putJax, d;
                for (var c in a)
    {
        if (a.hasOwnProperty(c))

        {
            if (a[c].length)
            {
                d = a[c];
                break
            }
        }
    }
    if (d && d.length)
    {
        if

        (b.renderer && b.renderer != = d[0].id)
        {
            d.unshift

            (MathJax.OutputJax[b.renderer])
            }
        b.renderer = d

        [0].id
            }
}, Hash:
                function()
{
    if

    (MathJax.Hub.config.positionToHash && document.locati

                on.hash && document.body && document.body.scrollIntoVie

                w)
                {
        var d = document.location.hash.substr(1);
        var

        f = document.getElementById(d);
        if (!f)
        {
            var

            c = document.getElementsByTagName("a");
            for (var

            e = 0, b = c.length; e < b; e++)
            {
                if (c[e].name == = d)
                {
                    f = c

                    [e];
                    break
            }
            }
        }
        if (f)
        {
            while (!f.scrollIntoView)

            {
                f = f.parentNode
            }
            f = this.HashCheck(f);
            if

            (f && f.scrollIntoView)
            {
                setTimeout(function()

                {
                    f.scrollIntoView(true)
            }, 1)
            }
        }
    }
}, HashCheck:
                function

                (b)
{
    if (b.isMathJax)
    {
        var a = MathJax.Hub.getJaxFor

        (b);
        if (a && MathJax.OutputJax

        [a.outputJax].hashCheck)
        {
            b = MathJax.OutputJax

            [a.outputJax].hashCheck(b)
            }
    }
    return

    b
            }, MenuZoom:
                function()
{
    if (!

    MathJax.Extension.MathMenu)
    {
        setTimeout

        (MathJax.Callback

        (["Require", MathJax.Ajax, "[MathJax]/extensions/Math

        Menu.js", {}]), 1000)
            }
    if (!

    MathJax.Extension.MathZoom)
    {
        setTimeout

        (MathJax.Callback

        (["Require", MathJax.Ajax, "[MathJax]/extensions/Math

        Zoom.js", {}]), 2000)
            }
}, onLoad:
                function()
{
    var

    a = this.onload = MathJax.Callback(function()

                {
        MathJax.Hub.Startup.signal.Post("onLoad")
            });
    if

    (document.body && document.readyState)
    {
        if

        (MathJax.Hub.Browser.isMSIE)
        {
            if

            (document.readyState == = "complete")
            {
                return[a]
            }
        }
        else

        {
            if (document.readyState != = "loading")
            {
                return[a]
            }
        }
    }

    if (window.addEventListener)

    {
        window.addEventListener("load", a, false);
        if (!

        this.params.noDOMContentEvent)

                {
            window.addEventListener

            ("DOMContentLoaded", a, false)
            }
    }
    else
    {
        if

        (window.attachEvent)
        {
            window.attachEvent

            ("onload", a)
            }
        else
        {
            window.onload = a
            }
    }
    return

    a
            }, Typeset:
                function(a, b)
{
    if

    (MathJax.Hub.config.skipStartupTypeset)
    {
        return

        function() { }
    }
    return this.queue.Push

      (["Post", this.signal, "Begin Typeset"],

                ["Typeset", MathJax.Hub, a, b],

                ["Post", this.signal, "End Typeset"])
            }, URL:
                function

                (b, a)
{
    if (!a.match( / ^ ([a - z] + : \ / \ / | \[ | \ / ) / ))

                {
        a = "[MathJax]/" + b + "/" + a
            }
    return

    a
            }, loadArray:
                function(b, f, c, a)
{
    if (b)
    {
        if (!(b

                instanceof Array))
                {
            b = [b]
            }
        if (b.length)
        {
            var

            h = MathJax.Callback.Queue(), j = { }, e;
            for (var

            g = 0, d = b.length; g < d; g++)
            {
                e = this.URL(f, b[g]);
                if (c)
                {
                    e

                    += "/" + c
            }
                if (a)
                {
                    h.Push

                    (["Require", MathJax.Ajax, e, j])
            }
        else
        {
            h.Push

            (MathJax.Ajax.Require(e, j))
            }
    }
    return h.Push( { })
            }
            }

                return null
            }
            };
                (function(d)
{
    var b = window[d], e = "[" + d

                             + "]";
    var c = b.Hub, a = b.Ajax, f = b.Callback;
    var

    g = MathJax.Object.Subclass

    ( {
    JAXFILE: "jax.js", require: null, config:

        { }, Init:
        function(i, h)
                {
            if (arguments.length == = 0)

            {
                return this
            }
            return (this.constructor.Subclass

            (i, h))()
            }, Augment:
        function(k, j)
                {
            var

            i = this.constructor, h = { };
            if (k != null)
            {
                for (var l in

                k)
                {
                    if (k.hasOwnProperty(l))
                    {
                        if (typeof k[l]
        
                        == = "function")
                        {
                            i.protoFunction(l, k[l])
                    }
                        else
                        {
                            h[l] = k

                            [l]
                    }
                    }
                }
                if (k.toString!

                == i.prototype.toString && k.toString != = { } .toString)

                {
                    i.protoFunction("toString", k.toString)
            }
            }
            c.Insert

            (i.prototype, h);
            i.Augment(null, j);
            return

            this
            }, Translate:
        function(h, i)
                {
            throw Error

            (this.directory + "/" + this.JAXFILE + " failed to define

            the Translate() method")
            }, Register:
        function(h)

                { }, Config:
        function()
                {
            this.config = c.CombineConfig

            (this.id, this.config);
            if (this.config.Augment)

            {
                this.Augment

                (this.config.Augment)
            }
        }, Startup:
        function()

                { }, loadComplete:
        function(i)
                {
            if (i == = "config.js")

            {
                return a.loadComplete(this.directory + "/" + i)
            }
            else

            {
                var h = f.Queue();
                h.Push(c.Register.StartupHook("End

                Config", {}), ["Post", c.Startup.signal, this.id + " Jax

                Config"], ["Config", this],

                ["Post", c.Startup.signal, this.id + " Jax Require"],

                [function(j)
                {
                    return MathJax.Hub.Startup.loadArray

                    (j.require, this.directory)
            }, this], [function(j, k)

                {
                return MathJax.Hub.Startup.loadArray

                 (j.extensions, "extensions/" + k)
            }, this.config ||

                {}, this.id], ["Post", c.Startup.signal, this.id + " Jax

                Startup"], ["Startup", this],

                ["Post", c.Startup.signal, this.id + " Jax Ready"]);
                if

                (this.copyTranslate)
                {
    h.Push([function(j)

                {
        j.preProcess = j.preTranslate;
        j.Process = j.Translate;

        j.postProcess = j.postTranslate
            }, this.constructor.pro

                totype])
            }
                return h.Push

                (["loadComplete", a, this.directory + "/" + i])
            }
            }
            },

                {
                id: "Jax", version: "2.1", directory: e

                + "/jax", extensionDir: e

                + "/extensions"
            });
                b.InputJax = g.Subclass

                ( {elementJax: "mml", copyTranslate: true, Process: funct

                ion(l, q)
{
    var j = f.Queue(), o;
    var

    k = this.elementJax;
    if (!(k instanceof Array))
                {
        k = [k]
            }

    for (var n = 0, h = k.length; n < h; n++)

    {
        o = b.ElementJax.directory + "/" + k

        [n] + "/" + this.JAXFILE;
        if (!this.require)

        {
            this.require = []
            }
        else
        {
            if (!(this.require instanceof

            Array))
                {
        this.require = [this.require]
            }
}

                this.require.push(o);
j.Push(a.Require(o))
            }

o = this.directory + "/" + this.JAXFILE;
                var p = j.Push

(a.Require(o));
                if (!p.called)

                {
    this.constructor.prototype.Process = function()
                {
        if

        (!p.called)
        {
            return p
            }
        throw Error(o + " failed to load

        properly")
            }
}
k = c.outputJax["jax/" + k[0]];
                if (k)

                {
    j.Push(a.Require(k[0].directory

    + "/" + this.JAXFILE))
            }
                return j.Push

                ( { })
            }, needsUpdate: function(h)
{
    var

    i = h.SourceElement();
    return (h.originalText!

     == b.HTML.getScript(i))
            }, Register: function(h)
{
    if (!

    c.inputJax)
    {
        c.inputJax = { }
    }
    c.inputJax[h] = this
            }
            },

                {
                id: "InputJax", version: "2.1", directory: g.directory

                + "/input", extensionDir: g.extensionDir
            });
                b.OutputJax

                = g.Subclass

                ( {copyTranslate: true, preProcess: function(j)
{
    var

    i, h = this.directory

    + "/" + this.JAXFILE;
    this.constructor.prototype.prePro

                cess = function(k)
                {
        if (!i.called)
        {
            return i
            }
        throw

        Error(h + " failed to load properly")
            };
    i = a.Require

    (h);
    return i
            }, Register: function(i)
{
    var

    h = c.outputJax;
    if (!h[i])
    {
        h[i] = []
            }
    if (h[i].length &&

    (this.id == = c.config.menuSettings.renderer ||

    (h.order[this.id] || 0) < (h.order[h[i][0].id] || 0)))
    {
        h

        [i].unshift(this)
            }
    else
    {
        h[i].push(this)
            }
    if (!

    this.require)
    {
        this.require = []
            }
    else
    {
        if (!

        (this.require instanceof Array))
                {
            this.require =

            [this.require]
            }
    }
    this.require.push

    (b.ElementJax.directory + "/" + (i.split( / \ //)

    [1]) + "/" + this.JAXFILE)
            }, Remove: function(h) { }
            },

                {
                id: "OutputJax", version: "2.1", directory: g.directory

                + "/output", extensionDir: g.extensionDir, fontDir: e

                + (b.isPacked? "" : "/..") + "/fonts", imageDir : e

                + (b.isPacked? "" : "/..") + "/images"
            });
                b.ElementJax = g.S

                ubclass( {
Init:
    function(i, h)
                {
        return

        this.constructor.Subclass

        (i, h)
            }, inputJax: null, outputJax: null, inputID: null, or

 iginalText: "", mimeType: "", Text:
    function(i, j)
                {
        var

        h = this.SourceElement();
        b.HTML.setScript

        (h, i);
        h.MathJax.state = this.STATE.UPDATE;
        return

        c.Update(h, j)
            }, Reprocess:
    function(i)
                {
        var

        h = this.SourceElement

        ();
        h.MathJax.state = this.STATE.UPDATE;
        return

        c.Reprocess(h, i)
            }, Update:
    function(h)
                {
        return

        this.Rerender(h)
            }, Rerender:
    function(i)
                {
        var

        h = this.SourceElement

        ();
        h.MathJax.state = this.STATE.OUTPUT;
        return

        c.Process(h, i)
            }, Remove:
    function(h)
                {
        if (this.hover)

        {
            this.hover.clear(this)
            }
        b.OutputJax

        [this.outputJax].Remove(this);
        if (!h)
        {
            c.signal.Post

            (["Remove Math", this.inputID]);
            this.Detach

            ()
            }
    }, needsUpdate:
    function()
                {
        return b.InputJax

        [this.inputJax].needsUpdate

        (this)
            }, SourceElement:
    function()
                {
        return

        document.getElementById

        (this.inputID)
            }, Attach:
    function(i, j)
                {
        var

        h = i.MathJax.elementJax;
        if

        (i.MathJax.state == = this.STATE.UPDATE)
        {
            h.Clone

            (this)
            }
        else
        {
            h = i.MathJax.elementJax = this;
            if (i.id)

            {
                this.inputID = i.id
            }
            else

            {
                i.id = this.inputID = b.ElementJax.GetID

                ();
                this.newID = 1
            }
        }
        h.originalText = b.HTML.getScript

        (i);
        h.inputJax = j;
        if (h.root)

        {
            h.root.inputID = h.inputID
            }
        return

        h
            }, Detach:
    function()
                {
        var h = this.SourceElement();
        if

        (!h)
        {
            return
            }
        try
        {
            delete h.MathJax
            }
        catch (i)

        {
            h.MathJax = null
            }
        if (this.newID)

        {
            h.id = ""
            }
    }, Clone:
    function(h)
                {
        var i;
        for (i in this)

        {
            if (!this.hasOwnProperty(i))
            {
                continue
            }
            if (typeof(h

            [i]) == = "undefined" && i != = "newID")
            {
                delete this[i]
            }
        }

        for (i in h)
        {
            if (!h.hasOwnProperty(i))
            {
                continue
            }
            if

            (typeof(this[i]) == = "undefined" || (this[i] != = h[i]

            && i != = "inputID"))
                {
            this[i] = h[i]
            }
    }
}
            },

                {
                id: "ElementJax", version: "2.1", directory: g.director

                y

                + "/element", extensionDir: g.extensionDir, ID: 0, STATE:

                {PENDING: 1, PROCESSED: 2, UPDATE: 3, OUTPUT: 4}, GetID: fun

                ction()
{
    this.ID++;
    return "MathJax-

                Element - " + this.ID
            }, Subclass: function()
{
    var

    h = g.Subclass.apply

    (this, arguments);
    h.loadComplete = this.prototype.load

                Complete;
    return

    h
            }
            });
                b.ElementJax.prototype.STATE = b.ElementJax.STAT

                E;
b.OutputJax.Error =

                {
                id: "Error"
                , version: "2.1"
                , config:

                {}, ContextMenu:
                function()
{
    return

    b.Extension.MathEvents.Event.ContextMenu.apply

    (b.Extension.MathEvents.Event, arguments)
            }, Mousedown

                :
                function()
{
    return

    b.Extension.MathEvents.Event.AltContextMenu.apply

    (b.Extension.MathEvents.Event, arguments)
            }, getJaxFro

 mMath:
                function()
{
    return

                {
    inputJax:
        "Error"
                , outputJax:
        "Error"
                , originalText:
        "M

                ath Processing Error"
            }
}
            };
                b.InputJax.Error =

                {
                id: "Error"
                , version: "2.1"
                , config:

                {}, sourceMenuTitle: "Error Message"
            }
            })("MathJax");

                (function(l)
{
    var f = window[l];
    if (!f)
    {
        f = window[l] =

                { }
    }
    var c = f.Hub;
    var q = c.Startup;
    var u = c.config;
    var

    e = document.getElementsByTagName("head")[0];
    if (!e)

    {
        e = document.childNodes[0]
            }
    var b =

    (document.documentElement ||

    document).getElementsByTagName("script");
    var d = new

    RegExp("(^|/)" + l + "\\.js(\\?.*)?$");
    for (var

    o = b.length - 1; o >= 0; o--)
    {
        if ((b[o].src || "").match(d))

        {
            q.script = b[o].innerHTML;
            if (RegExp.$2)
                {
        var

        r = RegExp.$2.substr(1).split( / \& / );
        for (var

        n = 0, h = r.length; n < h; n++)
        {
            var k = r[n].match( / (.*) =

            (.*) / );
            if (k)
            {
                q.params[unescape(k[1])] = unescape(k

                [2])
            }
            }
            }
                u.root = b[o].src.replace( / ( ^ | \ / )[ ^ \ / ] *

                (\ ? .*) ? $ / , "");
                break
            }
            }
                f.Ajax.config = u;
                var a = {isMac:

                (navigator.platform.substr(0, 3) == = "Mac"), isPC:

                (navigator.platform.substr(0, 3) == = "Win"), isMSIE:

                (window.ActiveXObject != null && window.clipboardData!

                = null), isFirefox:
                ((window.netscape != null ||

                window.mozPaintCount != null)

                && document.ATTRIBUTE_NODE != null && !

                window.opera), isSafari:
                (navigator.userAgent.match( /

                (Apple) ? WebKit\ //)!=null&&(!window.chrome||

                window.chrome.loadTimes == null)), isChrome :

                (window.chrome != null && window.chrome.loadTimes!

                = null), isOpera : (window.opera!

                = null && window.opera.version != null), isKonqueror :

                (window.hasOwnProperty && window.hasOwnProperty

                ("konqueror")

                && navigator.vendor == "KDE"), versionAtLeast : function

                (x)
                {
                var w = (this.version).split(".");
x = (new String

(x)).split(".");
                for (var y = 0, j = x.length; y<j; y++)
                {
                if

                (w[y] != x[y])
                {
                return parseInt(w[y] || "0") >= parseInt

                (x[y])
            }
            }
                return true
            }, Select:
                function(j)
{
    var i = j

    [c.Browser];
    if (i)
    {
        return i(c.Browser)
            }
    return

    null
            }
            };
                var g = navigator.userAgent.replace( / ^ Mozilla

                \ / (\d + \.) + \d + / , "").replace( / [a - z][-a - z0 - 9._: ] + \ /

                \d + [ ^ ] * -[ ^ ]*\.([a - z][a - z]) ? \d + / i, "").replace

                ( / Gentoo | Ubuntu\ / (\d + \.)*\d + (\([ ^ )]*\)

                ) ? / , ""); c.Browser = c.Insert(c.Insert(new String

                ("Unknown"), {version: "0.0"}), a); for (var t in a)
                {
                if

                (a.hasOwnProperty(t))
                {
                if (a[t] && t.substr(0, 2)

                == = "is")
                {
                t = t.slice(2);
                if (t == = "Mac" || t == = "PC")

                {
                continue
            }
                c.Browser = c.Insert(new String(t), a);
                var

                p = new RegExp(".*(Version)/((?:\\d+\\.)+\\d+)|.*

                (" + t + ")" + (t == "MSIE" ? " " : " / ") + "((?:\\d +\\.) *\\d +) |

(?:^|\\(| )([a-z]
[-a-z0-9._: ]+|(?:Apple)? WebKit)/

                 ((?:\\d+\\.)+\\d+)");
                 var s = p.exec(g) ||

                 ["", "", "", "unknown", "0.0"];
c.Browser.name = (s[1]

                 == "Version" ? t : (s[3] || s[5]));
                 c.Browser.version = s

                 [2] || s[4] || s[6];
                 break
             }
             }
             }
                 c.Browser.Select

                 ( {Safari:
                 function(j)
{
    var i = parseInt((String

    (j.version).split("."))[0]);
    if (i > 85)

    {
        j.webkit = j.version
             }
    if (i >= 534)
    {
        j.version = "5.1"
             }

    else
    {
        if (i >= 533)
        {
            j.version = "5.0"
             }
        else
        {
            if (i >= 526)

            {
                j.version = "4.0"
             }
            else
            {
                if (i >= 525)
                {
                    j.version = "3.1"
             }

                else
                {
                    if (i > 500)
                    {
                        j.version = "3.0"
                }
                    else
                    {
                        if (i > 400)

                        {
                            j.version = "2.0"
                    }
                        else
                        {
                            if (i > 85)

                            {
                                j.version = "1.0"
                        }
                        }
                    }
                }
            }
        }
    }
    j.isMobile =

    (navigator.appVersion.match( / Mobile / i)!

    = null);
    j.noContextMenu = j.isMobile
             }, Firefox:
                 function

                 (j)
{
    if ((j.version == = "0.0" ||

    navigator.userAgent.match( / Firefox / ) == null)

    && navigator.product == = "Gecko")
    {
        var

        m = navigator.userAgent.match( / [\ / ]rv: (\d + \.\d.* ? )

        [\) ] / ); if (m)
        {
            j.version = m[1]
             }
        else
        {
            var i =

            (navigator.buildID ||

            navigator.productSub || "0").substr(0, 8);
            if

            (i >= "20111220")
            {
                j.version = "9.0"
             }
            else
            {
                if

                (i >= "20111120")
                {
                    j.version = "8.0"
             }
                else
                {
                    if

                    (i >= "20110927")
                    {
                        j.version = "7.0"
                }
                    else
                    {
                        if

                        (i >= "20110816")
                        {
                            j.version = "6.0"
                    }
                        else
                        {
                            if

                            (i >= "20110621")
                            {
                                j.version = "5.0"
                        }
                            else
                            {
                                if

                                (i >= "20110320")
                                {
                                    j.version = "4.0"
                            }
                                else
                                {
                                    if

                                    (i >= "20100121")
                                    {
                                        j.version = "3.6"
                                }
                                    else
                                    {
                                        if

                                        (i >= "20090630")
                                        {
                                            j.version = "3.5"
                                    }
                                        else
                                        {
                                            if

                                            (i >= "20080617")
                                            {
                                                j.version = "3.0"
                                        }
                                            else
                                            {
                                                if

                                                (i >= "20061024")
                                                {
                                                    j.version = "2.0"
                                            }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    j.isMobile = (navigator.appVersion.match

    ( / Android / i) != null || navigator.userAgent.match( /

    Fennec\//)!=null)},Opera:function(i)

                 { i.version = opera.version()}, MSIE:
    function(j)

                 {
        j.isIE9 = !!(document.documentMode &&

        (window.performance ||

        window.msPerformance));
        MathJax.HTML.setScriptBug = !

        j.isIE9 || document.documentMode < 9;
        var v = false;
        try

        {
            new ActiveXObject

            ("MathPlayer.Factory.1");
            j.hasMathPlayer = v = true
             }

        catch (m) { }
        try
        {
            if (v && !q.params.NoMathPlayer)
                 {
                var

                i = document.createElement

                ("object");
                i.id = "mathplayer";
                i.classid = "clsid:32F66

                 A20 - 7614 - 11D4 - BD11 -

                     00104BD3F987";
                 document.getElementsByTagName

                 ("head")[0].appendChild(i);
                document.namespaces.add

("m", "http://www.w3.org/1998/Math/MathML");
                j.mpName

                space = true;
                if (document.readyState &&

                        (document.readyState == = "loading" ||

                                                  document.readyState == = "interactive"))

                {
                    document.write('<?import namespace="m"
    
                                   implementation = "#MathPlayer" > ');
                        j.mpImported = true
                }
            }

                           else
            {
                document.namespaces.add

                ("mjx_IE_fix", "http://www.w3.org/1999/xlink")
            }
        }

        catch (m) { }
    }
}); c.Browser.Select

    (MathJax.Message.browsers); c.queue = f.Callback.Queue

                                          (); c.queue.Push(["Post", q.signal, "Begin"],

                                                  ["Config", q], ["Cookie", q], ["Styles", q],

                                                  ["Message", q], function()
{
    var i = f.Callback.Queue

            (q.Jax(), q.Extensions());
    return i.Push( { })
    },

           ["Menu", q], q.onLoad(), function()

{ MathJax.isReady = true}, ["Typeset", q], ["Hash", q],

    ["MenuZoom", q], ["Post", q.signal, "End"])
})

("MathJax")
}
};

    }
}

    /*! jQuery UI - v1.10.1 - 2013-02-16
* http://jqueryui.com
* Includes: jquery.ui.core.js, jquery.ui.widget.js, jquery.ui.mouse.js, jquery.ui.position.js, jquery.ui.draggable.js, jquery.ui.resizable.js, jquery.ui.button.js, jquery.ui.datepicker.js, jquery.ui.dialog.js, jquery.ui.progressbar.js, jquery.ui.spinner.js, jquery.ui.tooltip.js, jquery.ui.effect.js, jquery.ui.effect-drop.js
* Copyright (c) 2013 jQuery Foundation and other contributors Licensed MIT */

(function(e, t) { function i(t, n){ var r, i, o, u = t.nodeName.toLowerCase(); return "area" === u ? (r = t.parentNode, i = r.name, !t.href || !i || r.nodeName.toLowerCase() !== "map" ? !1 : (o = e("img[usemap=#" + i + "]")[0], !!o && s(o))):(/ input | select | textarea | button | object /.test(u) ? !t.disabled : "a" === u ? t.href || n : n) && s(t)}
function s(t) { return e.expr.filters.visible(t) && !e(t).parents().addBack().filter(function(){ return e.css(this, "visibility") === "hidden"}).length}
var n = 0, r =/^ ui - id -\d+$/;e.ui=e.ui||{};if(e.ui.version)return;e.extend(e.ui,{version:"1.10.1",keyCode:{BACKSPACE:8,COMMA:188,DELETE:46,DOWN:40,END:35,ENTER:13,ESCAPE:27,HOME:36,LEFT:37,NUMPAD_ADD:107,NUMPAD_DECIMAL:110,NUMPAD_DIVIDE:111,NUMPAD_ENTER:108,NUMPAD_MULTIPLY:106,NUMPAD_SUBTRACT:109,PAGE_DOWN:34,PAGE_UP:33,PERIOD:190,RIGHT:39,SPACE:32,TAB:9,UP:38}}),e.fn.extend({_focus:e.fn.focus,focus:function(t, n) { return typeof t== "number" ? this.each(function(){ var r = this; setTimeout(function(){ e(r).focus(),n && n.call(r)},t)}):this._focus.apply(this, arguments)},scrollParent:function() { var t; return e.ui.ie &&/ (static| relative)/.test(this.css("position")) ||/ absolute /.test(this.css("position")) ? t = this.parents().filter(function(){ return/ (relative | absolute |fixed)/.test(e.css(this, "position")) &&/ (auto | scroll) /.test(e.css(this, "overflow") + e.css(this, "overflow-y") + e.css(this, "overflow-x"))}).eq(0):t = this.parents().filter(function(){ return/ (auto | scroll) /.test(e.css(this, "overflow") + e.css(this, "overflow-y") + e.css(this, "overflow-x"))}).eq(0),/fixed/.test(this.css("position")) || !t.length ? e(document) : t},zIndex:function(n) { if (n !== t) return this.css("zIndex", n); if (this.length) { var r = e(this[0]), i, s; while (r.length && r[0] !== document) { i = r.css("position"); if (i === "absolute" || i === "relative" || i === "fixed") { s = parseInt(r.css("zIndex"), 10); if (!isNaN(s) && s !== 0) return s} r = r.parent()} } return 0},uniqueId:function() { return this.each(function(){ this.id || (this.id = "ui-id-" + ++n)})},removeUniqueId:function() { return this.each(function(){ r.test(this.id) && e(this).removeAttr("id")})}}),e.extend(e.expr[":"],{data:e.expr.createPseudo? e.expr.createPseudo(function(t) { return function(n){ return !!e.data(n, t)} }):function(t, n, r) { return !!e.data(t, r[3])},focusable:function(t) { return i(t, !isNaN(e.attr(t, "tabindex")))},tabbable:function(t) { var n = e.attr(t, "tabindex"), r = isNaN(n); return (r || n >= 0) && i(t, !r)}}),e("<a>").outerWidth(1).jquery||e.each(["Width","Height"],function(n, r) { function u(t, n, r, s){ return e.each(i, function(){ n -= parseFloat(e.css(t, "padding" + this)) || 0, r && (n -= parseFloat(e.css(t, "border" + this + "Width")) || 0), s && (n -= parseFloat(e.css(t, "margin" + this)) || 0)}),n}var i = r === "Width"?["Left", "Right"]:["Top","Bottom"],s=r.toLowerCase(),o={innerWidth:e.fn.innerWidth,innerHeight:e.fn.innerHeight,outerWidth:e.fn.outerWidth,outerHeight:e.fn.outerHeight};e.fn["inner" + r]=function(n) { return n === t ? o["inner" + r].call(this) : this.each(function(){ e(this).css(s, u(this, n) + "px")})},e.fn["outer" + r]=function(t, n) { return typeof t!= "number" ? o["outer" + r].call(this, t) : this.each(function(){ e(this).css(s, u(this, t, !0, n) + "px")})}}),e.fn.addBack||(e.fn.addBack=function(e) { return this.add(e == null ? this.prevObject : this.prevObject.filter(e))}),e("<a>").data("a-b","a").removeData("a-b").data("a-b")&&(e.fn.removeData=function(t) { return function(n){ return arguments.length ? t.call(this, e.camelCase(n)) : t.call(this)} }(e.fn.removeData)),e.ui.ie=!!/msie[\w.]+/.exec(navigator.userAgent.toLowerCase()),e.support.selectstart="onselectstart"in document.createElement("div"),e.fn.extend({disableSelection:function() { return this.bind((e.support.selectstart ? "selectstart" : "mousedown") + ".ui-disableSelection", function(e){ e.preventDefault()})},enableSelection:function() { return this.unbind(".ui-disableSelection")}}),e.extend(e.ui,{plugin:{add:function(t, n, r) { var i, s = e.ui[t].prototype; for (i in r) s.plugins[i] = s.plugins[i] ||[],s.plugins[i].push([n, r[i]])},call:function(e, t, n) { var r, i = e.plugins[t]; if (!i || !e.element[0].parentNode || e.element[0].parentNode.nodeType === 11) return; for (r = 0; r < i.length; r++) e.options[i[r][0]] && i[r][1].apply(e.element, n)}},hasScroll:function(t, n) { if (e(t).css("overflow") === "hidden") return !1; var r = n && n === "left" ? "scrollLeft" : "scrollTop", i = !1; return t[r] > 0 ? !0 : (t[r] = 1,i = t[r] > 0,t[r] = 0,i)}})})(jQuery);(function(e, t) { var n = 0, r = Array.prototype.slice, i = e.cleanData; e.cleanData = function(t){ for (var n = 0, r; (r = t[n]) != null; n++) try { e(r).triggerHandler("remove")} catch (s) { } i(t)},e.widget = function(t, n, r){ var i, s, o, u, a = { }, f = t.split(".")[0]; t = t.split(".")[1],i = f + "-" + t,r || (r = n,n = e.Widget),e.expr[":"][i.toLowerCase()] = function(t){ return !!e.data(t, i)},e[f] = e[f] ||{ },s = e[f][t],o = e[f][t] = function(e, t){ if (!this._createWidget) return new o(e, t); arguments.length && this._createWidget(e, t)},e.extend(o, s,{ version: r.version,_proto: e.extend({ },r),_childConstructors:[]}),u = new n,u.options = e.widget.extend({ },u.options),e.each(r, function(t, r){ if (!e.isFunction(r)) { a[t] = r; return} a[t] = function(){ var e = function(){ return n.prototype[t].apply(this, arguments)},i = function(e){ return n.prototype[t].apply(this, e)}; return function(){ var t = this._super, n = this._superApply, s; return this._super = e,this._superApply = i,s = r.apply(this, arguments),this._super = t,this._superApply = n,s} } ()}),o.prototype = e.widget.extend(u,{ widgetEventPrefix: s? u.widgetEventPrefix: t},a,{ constructor: o,namespace:f,widgetName:t,widgetFullName:i }),s?(e.each(s._childConstructors,function(t, n) { var r = n.prototype; e.widget(r.namespace+"."+r.widgetName,o,n._proto)}),delete s._childConstructors):n._childConstructors.push(o),e.widget.bridge(t,o)},e.widget.extend=function(n) { var i = r.call(arguments, 1), s = 0, o = i.length, u, a; for (; s < o; s++) for (u in i[s]) a = i[s][u],i[s].hasOwnProperty(u) && a !== t && (e.isPlainObject(a) ? n[u] = e.isPlainObject(n[u]) ? e.widget.extend({ },n[u],a):e.widget.extend({ },a):n[u] = a); return n},e.widget.bridge=function(n, i) { var s = i.prototype.widgetFullName || n; e.fn[n] = function(o){ var u = typeof o== "string", a = r.call(arguments, 1), f = this; return o = !u && a.length ? e.widget.extend.apply(null,[o].concat(a)) : o,u ? this.each(function(){ var r, i = e.data(this, s); if (!i) return e.error("cannot call methods on " + n + " prior to initialization; " + "attempted to call method '" + o + "'"); if (!e.isFunction(i[o]) || o.charAt(0) === "_") return e.error("no such method '" + o + "' for " + n + " widget instance"); r = i[o].apply(i, a); if (r !== i && r !== t) return f = r && r.jquery ? f.pushStack(r.get()) : r,!1}):this.each(function(){ var t = e.data(this, s); t? t.option(o ||{ })._init():e.data(this, s, new i(o, this))}),f} },e.Widget=function() { },e.Widget._childConstructors=[],e.Widget.prototype={widgetName:"widget",widgetEventPrefix:"",defaultElement:"<div>",options:{disabled:!1,create:null},_createWidget:function(t, r) { r = e(r || this.defaultElement || this)[0],this.element = e(r),this.uuid = n++,this.eventNamespace = "." + this.widgetName + this.uuid,this.options = e.widget.extend({ },this.options,this._getCreateOptions(),t),this.bindings = e(),this.hoverable = e(),this.focusable = e(),r !== this && (e.data(r, this.widgetFullName, this),this._on(!0, this.element,{ remove: function(e){ e.target === r && this.destroy()} }),this.document = e(r.style ? r.ownerDocument : r.document || r),this.window = e(this.document[0].defaultView || this.document[0].parentWindow)),this._create(),this._trigger("create", null, this._getCreateEventData()),this._init()},_getCreateOptions:e.noop,_getCreateEventData:e.noop,_create:e.noop,_init:e.noop,destroy:function() { this._destroy(),this.element.unbind(this.eventNamespace).removeData(this.widgetName).removeData(this.widgetFullName).removeData(e.camelCase(this.widgetFullName)),this.widget().unbind(this.eventNamespace).removeAttr("aria-disabled").removeClass(this.widgetFullName + "-disabled " + "ui-state-disabled"),this.bindings.unbind(this.eventNamespace),this.hoverable.removeClass("ui-state-hover"),this.focusable.removeClass("ui-state-focus")},_destroy:e.noop,widget:function() { return this.element},option:function(n, r) { var i = n, s, o, u; if (arguments.length === 0) return e.widget.extend({ },this.options); if (typeof n== "string") { i ={ },s = n.split("."),n = s.shift(); if (s.length) { o = i[n] = e.widget.extend({ },this.options[n]); for (u = 0; u < s.length - 1; u++) o[s[u]] = o[s[u]] ||{ },o = o[s[u]]; n = s.pop(); if (r === t) return o[n] === t ? null : o[n]; o[n] = r} else { if (r === t) return this.options[n] === t ? null : this.options[n]; i[n] = r} } return this._setOptions(i),this},_setOptions:function(e) { var t; for (t in e) this._setOption(t, e[t]); return this},_setOption:function(e, t) { return this.options[e] = t,e === "disabled" && (this.widget().toggleClass(this.widgetFullName + "-disabled ui-state-disabled", !!t).attr("aria-disabled", t),this.hoverable.removeClass("ui-state-hover"),this.focusable.removeClass("ui-state-focus")),this},enable:function() { return this._setOption("disabled", !1)},disable:function() { return this._setOption("disabled", !0)},_on:function(t, n, r) { var i, s = this; typeof t!= "boolean" && (r = n,n = t,t = !1),r ? (n = i = e(n),this.bindings = this.bindings.add(n)):(r = n,n = this.element,i = this.widget()),e.each(r, function(r, o){ function u(){ if (!t && (s.options.disabled === !0 || e(this).hasClass("ui-state-disabled"))) return; return (typeof o== "string" ? s[o] : o).apply(s, arguments)} typeof o!= "string" && (u.guid = o.guid = o.guid || u.guid || e.guid++); var a = r.match(/^ (\w +)\s * (.*)$/),f = a[1] + s.eventNamespace,l = a[2]; l? i.delegate (l, f, u):n.bind(f, u)})},_off: function(e, t){ t = (t || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace,e.unbind(t).undelegate(t)},_delay: function(e, t){ function n(){ return (typeof e== "string" ? r[e] : e).apply(r, arguments)} var r = this; return setTimeout(n, t || 0)},_hoverable: function(t){ this.hoverable = this.hoverable.add(t),this._on(t,{ mouseenter: function(t){ e(t.currentTarget).addClass("ui-state-hover")},mouseleave: function(t){ e(t.currentTarget).removeClass("ui-state-hover")} })},_focusable: function(t){ this.focusable = this.focusable.add(t),this._on(t,{ focusin: function(t){ e(t.currentTarget).addClass("ui-state-focus")},focusout: function(t){ e(t.currentTarget).removeClass("ui-state-focus")} })},_trigger: function(t, n, r){ var i, s, o = this.options[t]; r = r ||{ },n = e.Event(n),n.type = (t === this.widgetEventPrefix ? t : this.widgetEventPrefix + t).toLowerCase(),n.target = this.element[0],s = n.originalEvent; if (s) for (i in s) i in n || (n[i] = s[i]); return this.element.trigger(n, r),!(e.isFunction(o) && o.apply(this.element[0],[n].concat(r)) === !1 || n.isDefaultPrevented())} },e.each({show:"fadeIn",hide:"fadeOut"},function(t, n) { e.Widget.prototype["_" + t] = function(r, i, s){ typeof i== "string" && (i ={ effect: i}); var o, u = i ? i === !0 || typeof i== "number" ? n : i.effect || n : t; i = i ||{ },typeof i== "number" && (i ={ duration: i}),o = !e.isEmptyObject(i),i.complete = s,i.delay && r.delay(i.delay),o && e.effects && e.effects.effect[u] ? r[t](i) : u !== t && r[u] ? r[u](i.duration, i.easing, s) : r.queue(function(n){ e(this)[t](),s && s.call(r[0]),n()})} })})(jQuery);(function(e, t) { var n = !1; e(document).mouseup(function(){ n = !1}),e.widget("ui.mouse",{ version: "1.10.1",options: { cancel: "input,textarea,button,select,option",distance: 1,delay: 0},_mouseInit: function(){ var t = this; this.element.bind("mousedown." + this.widgetName, function(e){ return t._mouseDown(e)}).bind("click." + this.widgetName, function(n){ if (!0 === e.data(n.target, t.widgetName + ".preventClickEvent")) return e.removeData(n.target, t.widgetName + ".preventClickEvent"),n.stopImmediatePropagation(),!1}),this.started = !1},_mouseDestroy: function(){ this.element.unbind("." + this.widgetName),this._mouseMoveDelegate && e(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate)},_mouseDown: function(t){ if (n) return; this._mouseStarted && this._mouseUp(t),this._mouseDownEvent = t; var r = this, i = t.which === 1, s = typeof this.options.cancel == "string" && t.target.nodeName ? e(t.target).closest(this.options.cancel).length : !1; if (!i || s || !this._mouseCapture(t)) return !0; this.mouseDelayMet = !this.options.delay,this.mouseDelayMet || (this._mouseDelayTimer = setTimeout(function(){ r.mouseDelayMet = !0},this.options.delay)); if (this._mouseDistanceMet(t) && this._mouseDelayMet(t)) { this._mouseStarted = this._mouseStart(t) !== !1; if (!this._mouseStarted) return t.preventDefault(),!0} return !0 === e.data(t.target, this.widgetName + ".preventClickEvent") && e.removeData(t.target, this.widgetName + ".preventClickEvent"),this._mouseMoveDelegate = function(e){ return r._mouseMove(e)},this._mouseUpDelegate = function(e){ return r._mouseUp(e)},e(document).bind("mousemove." + this.widgetName, this._mouseMoveDelegate).bind("mouseup." + this.widgetName, this._mouseUpDelegate),t.preventDefault(),n = !0,!0},_mouseMove: function(t){ return e.ui.ie && (!document.documentMode || document.documentMode < 9) && !t.button ? this._mouseUp(t) : this._mouseStarted ? (this._mouseDrag(t),t.preventDefault()):(this._mouseDistanceMet(t) && this._mouseDelayMet(t) && (this._mouseStarted = this._mouseStart(this._mouseDownEvent, t) !== !1,this._mouseStarted ? this._mouseDrag(t) : this._mouseUp(t)),!this._mouseStarted)},_mouseUp: function(t){ return e(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate),this._mouseStarted && (this._mouseStarted = !1,t.target === this._mouseDownEvent.target && e.data(t.target, this.widgetName + ".preventClickEvent", !0),this._mouseStop(t)),!1},_mouseDistanceMet: function(e){ return Math.max(Math.abs(this._mouseDownEvent.pageX - e.pageX), Math.abs(this._mouseDownEvent.pageY - e.pageY)) >= this.options.distance},_mouseDelayMet: function(){ return this.mouseDelayMet},_mouseStart: function(){ },_mouseDrag: function(){ },_mouseStop: function(){ },_mouseCapture: function(){ return !0} })})(jQuery);(function(e, t) { function h(e, t, n){ return[parseFloat(e[0]) * (l.test(e[0]) ? t / 100 : 1), parseFloat(e[1]) * (l.test(e[1]) ? n / 100 : 1)]}function p(t, n){ return parseInt(e.css(t, n), 10) || 0} function d(t){ var n = t[0]; return n.nodeType === 9 ?{ width: t.width(),height: t.height(),offset: { top: 0,left: 0} }:e.isWindow(n) ?{ width: t.width(),height: t.height(),offset: { top: t.scrollTop(),left: t.scrollLeft()} }:n.preventDefault ?{ width: 0,height: 0,offset: { top: n.pageY,left: n.pageX} }:{ width: t.outerWidth(),height: t.outerHeight(),offset: t.offset()} } e.ui = e.ui ||{ }; var n, r = Math.max, i = Math.abs, s = Math.round, o =/ left | center | right /, u =/ top | center | bottom /, a =/[\+\-]\d + (\.[\d]+)?%?/,f=/^\w+/,l=/%$/,c=e.fn.position;e.position={scrollbarWidth:function() { if (n !== t) return n; var r, i, s = e("<div style='display:block;width:50px;height:50px;overflow:hidden;'><div style='height:100px;width:auto;'></div></div>"), o = s.children()[0]; return e("body").append(s),r = o.offsetWidth,s.css("overflow", "scroll"),i = o.offsetWidth,r === i && (i = s[0].clientWidth),s.remove(),n = r - i},getScrollInfo:function(t) { var n = t.isWindow ? "" : t.element.css("overflow-x"), r = t.isWindow ? "" : t.element.css("overflow-y"), i = n === "scroll" || n === "auto" && t.width < t.element[0].scrollWidth, s = r === "scroll" || r === "auto" && t.height < t.element[0].scrollHeight; return{ width: i? e.position.scrollbarWidth():0,height: s? e.position.scrollbarWidth():0} },getWithinInfo:function(t) { var n = e(t || window), r = e.isWindow(n[0]); return{ element: n,isWindow: r,offset: n.offset() ||{ left: 0,top: 0},scrollLeft: n.scrollLeft(),scrollTop: n.scrollTop(),width: r? n.width():n.outerWidth(),height: r? n.height():n.outerHeight()} }},e.fn.position=function(t) { if (!t || !t.of) return c.apply(this, arguments); t = e.extend({ },t); var n, l, v, m, g, y, b = e(t.of), w = e.position.getWithinInfo(t.within), E = e.position.getScrollInfo(w), S = (t.collision || "flip").split(" "), x = { }; return y = d(b),b[0].preventDefault && (t.at = "left top"),l = y.width,v = y.height,m = y.offset,g = e.extend({ },m),e.each(["my", "at"],function(){ var e = (t[this] || "").split(" "), n, r; e.length === 1 && (e = o.test(e[0]) ? e.concat(["center"]):u.test(e[0])?["center"].concat(e):["center","center"]),e[0]=o.test(e[0])? e[0]:"center",e[1]=u.test(e[1])? e[1]:"center",n=a.exec(e[0]),r=a.exec(e[1]),x[this]=[n?n[0]:0,r? r[0]:0],t[this]=[f.exec(e[0])[0],f.exec(e[1])[0]]}),S.length===1&&(S[1]=S[0]),t.at[0]==="right"? g.left+=l:t.at[0]==="center"&&(g.left+=l/2),t.at[1]==="bottom"? g.top+=v:t.at[1]==="center"&&(g.top+=v/2),n=h(x.at, l, v),g.left+=n[0],g.top+=n[1],this.each(function(){ var o, u, a = e(this), f = a.outerWidth(), c = a.outerHeight(), d = p(this, "marginLeft"), y = p(this, "marginTop"), T = f + d + p(this, "marginRight") + E.width, N = c + y + p(this, "marginBottom") + E.height, C = e.extend({ },g),k = h(x.my, a.outerWidth(), a.outerHeight()); t.my[0] === "right" ? C.left -= f : t.my[0] === "center" && (C.left -= f / 2),t.my[1] === "bottom" ? C.top -= c : t.my[1] === "center" && (C.top -= c / 2),C.left += k[0],C.top += k[1],e.support.offsetFractions || (C.left = s(C.left),C.top = s(C.top)),o ={ marginLeft: d,marginTop: y},e.each(["left", "top"],function(r, i){ e.ui.position[S[r]] && e.ui.position[S[r]][i](C,{ targetWidth: l,targetHeight: v,elemWidth: f,elemHeight: c,collisionPosition: o,collisionWidth: T,collisionHeight: N,offset:[n[0] + k[0], n[1] + k[1]],my: t.my,at: t.at,within: w,elem: a})}),t.using&& (u = function(e){ var n = m.left - C.left, s = n + l - f, o = m.top - C.top, u = o + v - c, h = { target:{ element: b,left: m.left,top: m.top,width: l,height: v},element: { element: a,left: C.left,top: C.top,width: f,height: c},horizontal: s < 0 ? "left" : n > 0 ? "right" : "center",vertical: u < 0 ? "top" : o > 0 ? "bottom" : "middle"}; l < f && i(n + s) < l && (h.horizontal = "center"),v < c && i(o + u) < v && (h.vertical = "middle"),r(i(n), i(s)) > r(i(o), i(u)) ? h.important = "horizontal" : h.important = "vertical",t.using.call(this, e, h)}),a.offset(e.extend(C,{ using:u}))})},e.ui.position={fit:{left:function(e, t) { var n = t.within, i = n.isWindow ? n.scrollLeft : n.offset.left, s = n.width, o = e.left - t.collisionPosition.marginLeft, u = i - o, a = o + t.collisionWidth - s - i, f; t.collisionWidth > s ? u > 0 && a <= 0 ? (f = e.left + u + t.collisionWidth - s - i,e.left += u - f):a > 0 && u <= 0 ? e.left = i : u > a ? e.left = i + s - t.collisionWidth : e.left = i:u > 0 ? e.left += u : a > 0 ? e.left -= a : e.left = r(e.left - o, e.left)},top:function(e, t) { var n = t.within, i = n.isWindow ? n.scrollTop : n.offset.top, s = t.within.height, o = e.top - t.collisionPosition.marginTop, u = i - o, a = o + t.collisionHeight - s - i, f; t.collisionHeight > s ? u > 0 && a <= 0 ? (f = e.top + u + t.collisionHeight - s - i,e.top += u - f):a > 0 && u <= 0 ? e.top = i : u > a ? e.top = i + s - t.collisionHeight : e.top = i:u > 0 ? e.top += u : a > 0 ? e.top -= a : e.top = r(e.top - o, e.top)}},flip:{left:function(e, t) { var n = t.within, r = n.offset.left + n.scrollLeft, s = n.width, o = n.isWindow ? n.scrollLeft : n.offset.left, u = e.left - t.collisionPosition.marginLeft, a = u - o, f = u + t.collisionWidth - s - o, l = t.my[0] === "left" ? -t.elemWidth : t.my[0] === "right" ? t.elemWidth : 0, c = t.at[0] === "left" ? t.targetWidth : t.at[0] === "right" ? -t.targetWidth : 0, h = -2 * t.offset[0], p, d; if (a < 0) { p = e.left + l + c + h + t.collisionWidth - s - r; if (p < 0 || p < i(a)) e.left += l + c + h} else if (f > 0) { d = e.left - t.collisionPosition.marginLeft + l + c + h - o; if (d > 0 || i(d) < f) e.left += l + c + h} },top:function(e, t) { var n = t.within, r = n.offset.top + n.scrollTop, s = n.height, o = n.isWindow ? n.scrollTop : n.offset.top, u = e.top - t.collisionPosition.marginTop, a = u - o, f = u + t.collisionHeight - s - o, l = t.my[1] === "top", c = l ? -t.elemHeight : t.my[1] === "bottom" ? t.elemHeight : 0, h = t.at[1] === "top" ? t.targetHeight : t.at[1] === "bottom" ? -t.targetHeight : 0, p = -2 * t.offset[1], d, v; a < 0 ? (v = e.top + c + h + p + t.collisionHeight - s - r,e.top + c + h + p > a && (v < 0 || v < i(a)) && (e.top += c + h + p)):f > 0 && (d = e.top - t.collisionPosition.marginTop + c + h + p - o,e.top + c + h + p > f && (d > 0 || i(d) < f) && (e.top += c + h + p))}},flipfit:{left:function() { e.ui.position.flip.left.apply(this, arguments),e.ui.position.fit.left.apply(this, arguments)},top:function() { e.ui.position.flip.top.apply(this, arguments),e.ui.position.fit.top.apply(this, arguments)}}},function() { var t, n, r, i, s, o = document.getElementsByTagName("body")[0], u = document.createElement("div"); t = document.createElement(o ? "div" : "body"),r ={ visibility: "hidden",width: 0,height: 0,border: 0,margin: 0,background: "none"},o && e.extend(r,{ position: "absolute",left: "-1000px",top: "-1000px"}); for (s in r) t.style[s] = r[s]; t.appendChild(u),n = o || document.documentElement,n.insertBefore(t, n.firstChild),u.style.cssText = "position: absolute; left: 10.7432222px;",i = e(u).offset().left,e.support.offsetFractions = i > 10 && i < 11,t.innerHTML = "",n.removeChild(t)}()})(jQuery);(function(e, t) { e.widget("ui.draggable", e.ui.mouse,{ version: "1.10.1",widgetEventPrefix: "drag",options: { addClasses: !0,appendTo: "parent",axis: !1,connectToSortable: !1,containment: !1,cursor: "auto",cursorAt: !1,grid: !1,handle: !1,helper: "original",iframeFix: !1,opacity: !1,refreshPositions: !1,revert: !1,revertDuration: 500,scope: "default",scroll: !0,scrollSensitivity: 20,scrollSpeed: 20,snap: !1,snapMode: "both",snapTolerance: 20,stack: !1,zIndex: !1,drag: null,start: null,stop: null},_create: function(){ this.options.helper === "original" && !/^ (?: r | a | f) /.test(this.element.css("position")) && (this.element[0].style.position = "relative"),this.options.addClasses && this.element.addClass("ui-draggable"),this.options.disabled && this.element.addClass("ui-draggable-disabled"),this._mouseInit()},_destroy: function(){ this.element.removeClass("ui-draggable ui-draggable-dragging ui-draggable-disabled"),this._mouseDestroy()},_mouseCapture: function(t){ var n = this.options; return this.helper || n.disabled || e(t.target).closest(".ui-resizable-handle").length > 0 ? !1 : (this.handle = this._getHandle(t),this.handle ? (e(n.iframeFix === !0 ? "iframe" : n.iframeFix).each(function(){ e("<div class='ui-draggable-iframeFix' style='background: #fff;'></div>").css({ width: this.offsetWidth + "px",height: this.offsetHeight + "px",position: "absolute",opacity: "0.001",zIndex: 1e3}).css(e(this).offset()).appendTo("body")}),!0):!1)},_mouseStart: function(t){ var n = this.options; return this.helper = this._createHelper(t),this.helper.addClass("ui-draggable-dragging"),this._cacheHelperProportions(),e.ui.ddmanager && (e.ui.ddmanager.current = this),this._cacheMargins(),this.cssPosition = this.helper.css("position"),this.scrollParent = this.helper.scrollParent(),this.offset = this.positionAbs = this.element.offset(),this.offset ={ top: this.offset.top - this.margins.top,left: this.offset.left - this.margins.left},e.extend(this.offset,{ click: { left: t.pageX - this.offset.left,top: t.pageY - this.offset.top},parent: this._getParentOffset(),relative: this._getRelativeOffset()}),this.originalPosition = this.position = this._generatePosition(t),this.originalPageX = t.pageX,this.originalPageY = t.pageY,n.cursorAt && this._adjustOffsetFromHelper(n.cursorAt),n.containment && this._setContainment(),this._trigger("start", t) === !1 ? (this._clear(),!1):(this._cacheHelperProportions(),e.ui.ddmanager && !n.dropBehaviour && e.ui.ddmanager.prepareOffsets(this, t),this._mouseDrag(t, !0),e.ui.ddmanager && e.ui.ddmanager.dragStart(this, t),!0)},_mouseDrag: function(t, n){ this.position = this._generatePosition(t),this.positionAbs = this._convertPositionTo("absolute"); if (!n) { var r = this._uiHash(); if (this._trigger("drag", t, r) === !1) return this._mouseUp({ }),!1; this.position = r.position} if (!this.options.axis || this.options.axis !== "y") this.helper[0].style.left = this.position.left + "px"; if (!this.options.axis || this.options.axis !== "x") this.helper[0].style.top = this.position.top + "px"; return e.ui.ddmanager && e.ui.ddmanager.drag(this, t),!1},_mouseStop: function(t){ var n, r = this, i = !1, s = !1; e.ui.ddmanager && !this.options.dropBehaviour && (s = e.ui.ddmanager.drop(this, t)),this.dropped && (s = this.dropped,this.dropped = !1),n = this.element[0]; while (n && (n = n.parentNode)) n === document && (i = !0); return !i && this.options.helper === "original" ? !1 : (this.options.revert === "invalid" && !s || this.options.revert === "valid" && s || this.options.revert === !0 || e.isFunction(this.options.revert) && this.options.revert.call(this.element, s) ? e(this.helper).animate(this.originalPosition, parseInt(this.options.revertDuration, 10), function(){ r._trigger("stop", t) !== !1 && r._clear()}):this._trigger("stop", t) !== !1 && this._clear(),!1)},_mouseUp: function(t){ return e("div.ui-draggable-iframeFix").each(function(){ this.parentNode.removeChild(this)}),e.ui.ddmanager && e.ui.ddmanager.dragStop(this, t),e.ui.mouse.prototype._mouseUp.call(this, t)},cancel: function(){ return this.helper.is(".ui-draggable-dragging") ? this._mouseUp({ }):this._clear(),this},_getHandle: function(t){ var n = !this.options.handle || !e(this.options.handle, this.element).length ? !0 : !1; return e(this.options.handle, this.element).find("*").addBack().each(function(){ this === t.target && (n = !0)}),n},_createHelper: function(t){ var n = this.options, r = e.isFunction(n.helper) ? e(n.helper.apply(this.element[0],[t])) : n.helper === "clone" ? this.element.clone().removeAttr("id") : this.element; return r.parents("body").length || r.appendTo(n.appendTo === "parent" ? this.element[0].parentNode : n.appendTo),r[0] !== this.element[0] && !/ (fixed| absolute)/.test(r.css("position")) && r.css("position", "absolute"),r},_adjustOffsetFromHelper: function(t){ typeof t== "string" && (t = t.split(" ")),e.isArray(t) && (t ={ left: +t[0],top: +t[1] || 0}),"left"in t && (this.offset.click.left = t.left + this.margins.left),"right"in t && (this.offset.click.left = this.helperProportions.width - t.right + this.margins.left),"top"in t && (this.offset.click.top = t.top + this.margins.top),"bottom"in t && (this.offset.click.top = this.helperProportions.height - t.bottom + this.margins.top)},_getParentOffset: function(){ this.offsetParent = this.helper.offsetParent(); var t = this.offsetParent.offset(); this.cssPosition === "absolute" && this.scrollParent[0] !== document && e.contains(this.scrollParent[0], this.offsetParent[0]) && (t.left += this.scrollParent.scrollLeft(),t.top += this.scrollParent.scrollTop()); if (this.offsetParent[0] === document.body || this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() === "html" && e.ui.ie) t ={ top: 0,left: 0}; return{ top: t.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0),left: t.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)} },_getRelativeOffset: function(){ if (this.cssPosition === "relative") { var e = this.element.position(); return{ top: e.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(),left: e.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft()} } return{ top: 0,left: 0} },_cacheMargins: function(){ this.margins ={ left: parseInt(this.element.css("marginLeft"), 10) || 0,top: parseInt(this.element.css("marginTop"), 10) || 0,right: parseInt(this.element.css("marginRight"), 10) || 0,bottom: parseInt(this.element.css("marginBottom"), 10) || 0} },_cacheHelperProportions: function(){ this.helperProportions ={ width: this.helper.outerWidth(),height: this.helper.outerHeight()} },_setContainment: function(){ var t, n, r, i = this.options; i.containment === "parent" && (i.containment = this.helper[0].parentNode); if (i.containment === "document" || i.containment === "window") this.containment =[i.containment === "document" ? 0 : e(window).scrollLeft() - this.offset.relative.left - this.offset.parent.left, i.containment === "document" ? 0 : e(window).scrollTop() - this.offset.relative.top - this.offset.parent.top, (i.containment === "document" ? 0 : e(window).scrollLeft()) + e(i.containment === "document" ? document : window).width() - this.helperProportions.width - this.margins.left, (i.containment === "document" ? 0 : e(window).scrollTop()) + (e(i.containment === "document" ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]; if (!/^ (document | window | parent)$/.test(i.containment) && i.containment.constructor !== Array){ n = e(i.containment),r = n[0]; if (!r) return; t = e(r).css("overflow") !== "hidden",this.containment =[(parseInt(e(r).css("borderLeftWidth"), 10) || 0) + (parseInt(e(r).css("paddingLeft"), 10) || 0), (parseInt(e(r).css("borderTopWidth"), 10) || 0) + (parseInt(e(r).css("paddingTop"), 10) || 0), (t ? Math.max(r.scrollWidth, r.offsetWidth) : r.offsetWidth) - (parseInt(e(r).css("borderLeftWidth"), 10) || 0) - (parseInt(e(r).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left - this.margins.right, (t ? Math.max(r.scrollHeight, r.offsetHeight) : r.offsetHeight) - (parseInt(e(r).css("borderTopWidth"), 10) || 0) - (parseInt(e(r).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top - this.margins.bottom],this.relative_container = n}else i.containment.constructor === Array && (this.containment = i.containment)},_convertPositionTo: function(t, n){ n || (n = this.position); var r = t === "absolute" ? 1 : -1, i = this.cssPosition !== "absolute" || this.scrollParent[0] !== document && !!e.contains(this.scrollParent[0], this.offsetParent[0]) ? this.scrollParent : this.offsetParent, s =/ (html | body) / i.test(i[0].tagName); return{ top: n.top + this.offset.relative.top * r + this.offset.parent.top * r - (this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : s ? 0 : i.scrollTop()) * r,left: n.left + this.offset.relative.left * r + this.offset.parent.left * r - (this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : s ? 0 : i.scrollLeft()) * r} },_generatePosition: function(t){ var n, r, i, s, o = this.options, u = this.cssPosition !== "absolute" || this.scrollParent[0] !== document && !!e.contains(this.scrollParent[0], this.offsetParent[0]) ? this.scrollParent : this.offsetParent, a =/ (html | body) / i.test(u[0].tagName), f = t.pageX, l = t.pageY; return this.originalPosition && (this.containment && (this.relative_container ? (r = this.relative_container.offset(),n =[this.containment[0] + r.left, this.containment[1] + r.top, this.containment[2] + r.left, this.containment[3] + r.top]):n = this.containment,t.pageX - this.offset.click.left < n[0] && (f = n[0] + this.offset.click.left),t.pageY - this.offset.click.top < n[1] && (l = n[1] + this.offset.click.top),t.pageX - this.offset.click.left > n[2] && (f = n[2] + this.offset.click.left),t.pageY - this.offset.click.top > n[3] && (l = n[3] + this.offset.click.top)),o.grid && (i = o.grid[1] ? this.originalPageY + Math.round((l - this.originalPageY) / o.grid[1]) * o.grid[1] : this.originalPageY,l = n ? i - this.offset.click.top >= n[1] || i - this.offset.click.top > n[3] ? i : i - this.offset.click.top >= n[1] ? i - o.grid[1] : i + o.grid[1] : i,s = o.grid[0] ? this.originalPageX + Math.round((f - this.originalPageX) / o.grid[0]) * o.grid[0] : this.originalPageX,f = n ? s - this.offset.click.left >= n[0] || s - this.offset.click.left > n[2] ? s : s - this.offset.click.left >= n[0] ? s - o.grid[0] : s + o.grid[0] : s)),{ top: l - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + (this.cssPosition === "fixed" ? -this.scrollParent.scrollTop() : a ? 0 : u.scrollTop()),left: f - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + (this.cssPosition === "fixed" ? -this.scrollParent.scrollLeft() : a ? 0 : u.scrollLeft())} },_clear: function(){ this.helper.removeClass("ui-draggable-dragging"),this.helper[0] !== this.element[0] && !this.cancelHelperRemoval && this.helper.remove(),this.helper = null,this.cancelHelperRemoval = !1},_trigger: function(t, n, r){ return r = r || this._uiHash(),e.ui.plugin.call(this, t,[n, r]),t === "drag" && (this.positionAbs = this._convertPositionTo("absolute")),e.Widget.prototype._trigger.call(this, t, n, r)},plugins: { },_uiHash: function(){ return{ helper: this.helper,position: this.position,originalPosition: this.originalPosition,offset: this.positionAbs} } }),e.ui.plugin.add("draggable", "connectToSortable",{ start: function(t, n){ var r = e(this).data("ui-draggable"), i = r.options, s = e.extend({ },n,{ item: r.element}); r.sortables =[],e(i.connectToSortable).each(function(){ var n = e.data(this, "ui-sortable"); n && !n.options.disabled && (r.sortables.push({ instance: n,shouldRevert: n.options.revert}),n.refreshPositions(),n._trigger("activate", t, s))})},stop: function(t, n){ var r = e(this).data("ui-draggable"), i = e.extend({ },n,{ item: r.element}); e.each(r.sortables, function(){ this.instance.isOver ? (this.instance.isOver = 0,r.cancelHelperRemoval = !0,this.instance.cancelHelperRemoval = !1,this.shouldRevert && (this.instance.options.revert = !0),this.instance._mouseStop(t),this.instance.options.helper = this.instance.options._helper,r.options.helper === "original" && this.instance.currentItem.css({ top: "auto",left: "auto"})):(this.instance.cancelHelperRemoval = !1,this.instance._trigger("deactivate", t, i))})},drag: function(t, n){ var r = e(this).data("ui-draggable"), i = this; e.each(r.sortables, function(){ var s = !1, o = this; this.instance.positionAbs = r.positionAbs,this.instance.helperProportions = r.helperProportions,this.instance.offset.click = r.offset.click,this.instance._intersectsWith(this.instance.containerCache) && (s = !0,e.each(r.sortables, function(){ return this.instance.positionAbs = r.positionAbs,this.instance.helperProportions = r.helperProportions,this.instance.offset.click = r.offset.click,this !== o && this.instance._intersectsWith(this.instance.containerCache) && e.contains(o.instance.element[0], this.instance.element[0]) && (s = !1),s})),s ? (this.instance.isOver || (this.instance.isOver = 1,this.instance.currentItem = e(i).clone().removeAttr("id").appendTo(this.instance.element).data("ui-sortable-item", !0),this.instance.options._helper = this.instance.options.helper,this.instance.options.helper = function(){ return n.helper[0]},t.target = this.instance.currentItem[0],this.instance._mouseCapture(t, !0),this.instance._mouseStart(t, !0, !0),this.instance.offset.click.top = r.offset.click.top,this.instance.offset.click.left = r.offset.click.left,this.instance.offset.parent.left -= r.offset.parent.left - this.instance.offset.parent.left,this.instance.offset.parent.top -= r.offset.parent.top - this.instance.offset.parent.top,r._trigger("toSortable", t),r.dropped = this.instance.element,r.currentItem = r.element,this.instance.fromOutside = r),this.instance.currentItem && this.instance._mouseDrag(t)):this.instance.isOver && (this.instance.isOver = 0,this.instance.cancelHelperRemoval = !0,this.instance.options.revert = !1,this.instance._trigger("out", t, this.instance._uiHash(this.instance)),this.instance._mouseStop(t, !0),this.instance.options.helper = this.instance.options._helper,this.instance.currentItem.remove(),this.instance.placeholder && this.instance.placeholder.remove(),r._trigger("fromSortable", t),r.dropped = !1)})} }),e.ui.plugin.add("draggable", "cursor",{ start: function(){ var t = e("body"), n = e(this).data("ui-draggable").options; t.css("cursor") && (n._cursor = t.css("cursor")),t.css("cursor", n.cursor)},stop: function(){ var t = e(this).data("ui-draggable").options; t._cursor && e("body").css("cursor", t._cursor)} }),e.ui.plugin.add("draggable", "opacity",{ start: function(t, n){ var r = e(n.helper), i = e(this).data("ui-draggable").options; r.css("opacity") && (i._opacity = r.css("opacity")),r.css("opacity", i.opacity)},stop: function(t, n){ var r = e(this).data("ui-draggable").options; r._opacity && e(n.helper).css("opacity", r._opacity)} }),e.ui.plugin.add("draggable", "scroll",{ start: function(){ var t = e(this).data("ui-draggable"); t.scrollParent[0] !== document && t.scrollParent[0].tagName !== "HTML" && (t.overflowOffset = t.scrollParent.offset())},drag: function(t){ var n = e(this).data("ui-draggable"), r = n.options, i = !1; if (n.scrollParent[0] !== document && n.scrollParent[0].tagName !== "HTML") { if (!r.axis || r.axis !== "x") n.overflowOffset.top + n.scrollParent[0].offsetHeight - t.pageY < r.scrollSensitivity ? n.scrollParent[0].scrollTop = i = n.scrollParent[0].scrollTop + r.scrollSpeed : t.pageY - n.overflowOffset.top < r.scrollSensitivity && (n.scrollParent[0].scrollTop = i = n.scrollParent[0].scrollTop - r.scrollSpeed); if (!r.axis || r.axis !== "y") n.overflowOffset.left + n.scrollParent[0].offsetWidth - t.pageX < r.scrollSensitivity ? n.scrollParent[0].scrollLeft = i = n.scrollParent[0].scrollLeft + r.scrollSpeed : t.pageX - n.overflowOffset.left < r.scrollSensitivity && (n.scrollParent[0].scrollLeft = i = n.scrollParent[0].scrollLeft - r.scrollSpeed)} else { if (!r.axis || r.axis !== "x") t.pageY - e(document).scrollTop() < r.scrollSensitivity ? i = e(document).scrollTop(e(document).scrollTop() - r.scrollSpeed) : e(window).height() - (t.pageY - e(document).scrollTop()) < r.scrollSensitivity && (i = e(document).scrollTop(e(document).scrollTop() + r.scrollSpeed)); if (!r.axis || r.axis !== "y") t.pageX - e(document).scrollLeft() < r.scrollSensitivity ? i = e(document).scrollLeft(e(document).scrollLeft() - r.scrollSpeed) : e(window).width() - (t.pageX - e(document).scrollLeft()) < r.scrollSensitivity && (i = e(document).scrollLeft(e(document).scrollLeft() + r.scrollSpeed))} i !== !1 && e.ui.ddmanager && !r.dropBehaviour && e.ui.ddmanager.prepareOffsets(n, t)} }),e.ui.plugin.add("draggable", "snap",{ start: function(){ var t = e(this).data("ui-draggable"), n = t.options; t.snapElements =[],e(n.snap.constructor !== String ? n.snap.items || ":data(ui-draggable)" : n.snap).each(function(){ var n = e(this), r = n.offset(); this !== t.element[0] && t.snapElements.push({ item: this,width: n.outerWidth(),height: n.outerHeight(),top: r.top,left: r.left})})},drag: function(t, n){ var r, i, s, o, u, a, f, l, c, h, p = e(this).data("ui-draggable"), d = p.options, v = d.snapTolerance, m = n.offset.left, g = m + p.helperProportions.width, y = n.offset.top, b = y + p.helperProportions.height; for (c = p.snapElements.length - 1; c >= 0; c--) { u = p.snapElements[c].left,a = u + p.snapElements[c].width,f = p.snapElements[c].top,l = f + p.snapElements[c].height; if (!(u - v < m && m < a + v && f - v < y && y < l + v || u - v < m && m < a + v && f - v < b && b < l + v || u - v < g && g < a + v && f - v < y && y < l + v || u - v < g && g < a + v && f - v < b && b < l + v)) { p.snapElements[c].snapping && p.options.snap.release && p.options.snap.release.call(p.element, t, e.extend(p._uiHash(),{ snapItem: p.snapElements[c].item})),p.snapElements[c].snapping = !1; continue} d.snapMode !== "inner" && (r = Math.abs(f - b) <= v,i = Math.abs(l - y) <= v,s = Math.abs(u - g) <= v,o = Math.abs(a - m) <= v,r && (n.position.top = p._convertPositionTo("relative",{ top: f - p.helperProportions.height,left: 0}).top - p.margins.top),i && (n.position.top = p._convertPositionTo("relative",{ top: l,left: 0}).top - p.margins.top),s && (n.position.left = p._convertPositionTo("relative",{ top: 0,left: u - p.helperProportions.width}).left - p.margins.left),o && (n.position.left = p._convertPositionTo("relative",{ top: 0,left: a}).left - p.margins.left)),h = r || i || s || o,d.snapMode !== "outer" && (r = Math.abs(f - y) <= v,i = Math.abs(l - b) <= v,s = Math.abs(u - m) <= v,o = Math.abs(a - g) <= v,r && (n.position.top = p._convertPositionTo("relative",{ top: f,left: 0}).top - p.margins.top),i && (n.position.top = p._convertPositionTo("relative",{ top: l - p.helperProportions.height,left: 0}).top - p.margins.top),s && (n.position.left = p._convertPositionTo("relative",{ top: 0,left: u}).left - p.margins.left),o && (n.position.left = p._convertPositionTo("relative",{ top: 0,left: a - p.helperProportions.width}).left - p.margins.left)),!p.snapElements[c].snapping && (r || i || s || o || h) && p.options.snap.snap && p.options.snap.snap.call(p.element, t, e.extend(p._uiHash(),{ snapItem: p.snapElements[c].item})),p.snapElements[c].snapping = r || i || s || o || h} }}),e.ui.plugin.add("draggable","stack",{start:function() { var t, n = this.data("ui-draggable").options, r = e.makeArray(e(n.stack)).sort(function(t, n){ return (parseInt(e(t).css("zIndex"), 10) || 0) - (parseInt(e(n).css("zIndex"), 10) || 0)}); if (!r.length) return; t = parseInt(e(r[0]).css("zIndex"), 10) || 0,e(r).each(function(n){ e(this).css("zIndex", t + n)}),this.css("zIndex", t + r.length)}}),e.ui.plugin.add("draggable","zIndex",{start:function(t, n) { var r = e(n.helper), i = e(this).data("ui-draggable").options; r.css("zIndex") && (i._zIndex = r.css("zIndex")),r.css("zIndex", i.zIndex)},stop:function(t, n) { var r = e(this).data("ui-draggable").options; r._zIndex && e(n.helper).css("zIndex", r._zIndex)}})})(jQuery);(function(e, t) { function n(e){ return parseInt(e, 10) || 0} function r(e){ return !isNaN(parseInt(e, 10))} e.widget("ui.resizable", e.ui.mouse,{ version: "1.10.1",widgetEventPrefix: "resize",options: { alsoResize: !1,animate: !1,animateDuration: "slow",animateEasing: "swing",aspectRatio: !1,autoHide: !1,containment: !1,ghost: !1,grid: !1,handles: "e,s,se",helper: !1,maxHeight: null,maxWidth: null,minHeight: 10,minWidth: 10,zIndex: 90,resize: null,start: null,stop: null},_create: function(){ var t, n, r, i, s, o = this, u = this.options; this.element.addClass("ui-resizable"),e.extend(this,{ _aspectRatio: !!u.aspectRatio,aspectRatio: u.aspectRatio,originalElement: this.element,_proportionallyResizeElements:[],_helper: u.helper || u.ghost || u.animate ? u.helper || "ui-resizable-helper" : null}),this.element[0].nodeName.match(/ canvas | textarea | input | select | button | img / i) && (this.element.wrap(e("<div class='ui-wrapper' style='overflow: hidden;'></div>").css({ position: this.element.css("position"),width: this.element.outerWidth(),height: this.element.outerHeight(),top: this.element.css("top"),left: this.element.css("left")})),this.element = this.element.parent().data("ui-resizable", this.element.data("ui-resizable")),this.elementIsWrapper = !0,this.element.css({ marginLeft: this.originalElement.css("marginLeft"),marginTop: this.originalElement.css("marginTop"),marginRight: this.originalElement.css("marginRight"),marginBottom: this.originalElement.css("marginBottom")}),this.originalElement.css({ marginLeft: 0,marginTop: 0,marginRight: 0,marginBottom: 0}),this.originalResizeStyle = this.originalElement.css("resize"),this.originalElement.css("resize", "none"),this._proportionallyResizeElements.push(this.originalElement.css({ position: "static",zoom: 1,display: "block"})),this.originalElement.css({ margin: this.originalElement.css("margin")}),this._proportionallyResize()),this.handles = u.handles || (e(".ui-resizable-handle", this.element).length ?{ n: ".ui-resizable-n",e: ".ui-resizable-e",s: ".ui-resizable-s",w: ".ui-resizable-w",se: ".ui-resizable-se",sw: ".ui-resizable-sw",ne: ".ui-resizable-ne",nw: ".ui-resizable-nw"}:"e,s,se"); if (this.handles.constructor === String) { this.handles === "all" && (this.handles = "n,e,s,w,se,sw,ne,nw"),t = this.handles.split(","),this.handles ={ }; for (n = 0; n < t.length; n++) r = e.trim(t[n]),s = "ui-resizable-" + r,i = e("<div class='ui-resizable-handle " + s + "'></div>"),i.css({ zIndex: u.zIndex}),"se" === r && i.addClass("ui-icon ui-icon-gripsmall-diagonal-se"),this.handles[r] = ".ui-resizable-" + r,this.element.append(i)} this._renderAxis = function(t){ var n, r, i, s; t = t || this.element; for (n in this.handles) { this.handles[n].constructor === String && (this.handles[n] = e(this.handles[n], this.element).show()),this.elementIsWrapper && this.originalElement[0].nodeName.match(/ textarea | input | select | button / i) && (r = e(this.handles[n], this.element),s =/ sw | ne | nw | se | n | s /.test(n) ? r.outerHeight() : r.outerWidth(),i =["padding",/ ne | nw | n /.test(n) ? "Top" :/ se | sw | s /.test(n) ? "Bottom" :/^ e$/.test(n) ? "Right" : "Left"].join(""),t.css(i, s),this._proportionallyResize()); if (!e(this.handles[n]).length) continue} },this._renderAxis(this.element),this._handles = e(".ui-resizable-handle", this.element).disableSelection(),this._handles.mouseover(function(){ o.resizing || (this.className && (i = this.className.match(/ ui - resizable - (se | sw | ne | nw | n | e | s | w) / i)),o.axis = i && i[1] ? i[1] : "se")}),u.autoHide && (this._handles.hide(),e(this.element).addClass("ui-resizable-autohide").mouseenter(function(){ if (u.disabled) return; e(this).removeClass("ui-resizable-autohide"),o._handles.show()}).mouseleave(function(){ if (u.disabled) return; o.resizing || (e(this).addClass("ui-resizable-autohide"),o._handles.hide())})),this._mouseInit()},_destroy: function(){ this._mouseDestroy(); var t, n = function(t){ e(t).removeClass("ui-resizable ui-resizable-disabled ui-resizable-resizing").removeData("resizable").removeData("ui-resizable").unbind(".resizable").find(".ui-resizable-handle").remove()}; return this.elementIsWrapper && (n(this.element),t = this.element,this.originalElement.css({ position: t.css("position"),width: t.outerWidth(),height: t.outerHeight(),top: t.css("top"),left: t.css("left")}).insertAfter(t),t.remove()),this.originalElement.css("resize", this.originalResizeStyle),n(this.originalElement),this},_mouseCapture: function(t){ var n, r, i = !1; for (n in this.handles) { r = e(this.handles[n])[0]; if (r === t.target || e.contains(r, t.target)) i = !0} return !this.options.disabled && i},_mouseStart: function(t){ var r, i, s, o = this.options, u = this.element.position(), a = this.element; return this.resizing = !0,/ absolute /.test(a.css("position")) ? a.css({ position: "absolute",top: a.css("top"),left: a.css("left")}):a.is(".ui-draggable") && a.css({ position: "absolute",top: u.top,left: u.left}),this._renderProxy(),r = n(this.helper.css("left")),i = n(this.helper.css("top")),o.containment && (r += e(o.containment).scrollLeft() || 0,i += e(o.containment).scrollTop() || 0),this.offset = this.helper.offset(),this.position ={ left: r,top: i},this.size = this._helper ?{ width: a.outerWidth(),height: a.outerHeight()}:{ width: a.width(),height: a.height()},this.originalSize = this._helper ?{ width: a.outerWidth(),height: a.outerHeight()}:{ width: a.width(),height: a.height()},this.originalPosition ={ left: r,top: i},this.sizeDiff ={ width: a.outerWidth() - a.width(),height: a.outerHeight() - a.height()},this.originalMousePosition ={ left: t.pageX,top: t.pageY},this.aspectRatio = typeof o.aspectRatio== "number" ? o.aspectRatio : this.originalSize.width / this.originalSize.height || 1,s = e(".ui-resizable-" + this.axis).css("cursor"),e("body").css("cursor", s === "auto" ? this.axis + "-resize" : s),a.addClass("ui-resizable-resizing"),this._propagate("start", t),!0},_mouseDrag: function(t){ var n, r = this.helper, i = { }, s = this.originalMousePosition, o = this.axis, u = this.position.top, a = this.position.left, f = this.size.width, l = this.size.height, c = t.pageX - s.left || 0, h = t.pageY - s.top || 0, p = this._change[o]; if (!p) return !1; n = p.apply(this,[t, c, h]),this._updateVirtualBoundaries(t.shiftKey); if (this._aspectRatio || t.shiftKey) n = this._updateRatio(n, t); return n = this._respectSize(n, t),this._updateCache(n),this._propagate("resize", t),this.position.top !== u && (i.top = this.position.top + "px"),this.position.left !== a && (i.left = this.position.left + "px"),this.size.width !== f && (i.width = this.size.width + "px"),this.size.height !== l && (i.height = this.size.height + "px"),r.css(i),!this._helper && this._proportionallyResizeElements.length && this._proportionallyResize(),e.isEmptyObject(i) || this._trigger("resize", t, this.ui()),!1},_mouseStop: function(t){ this.resizing = !1; var n, r, i, s, o, u, a, f = this.options, l = this; return this._helper && (n = this._proportionallyResizeElements,r = n.length &&/ textarea / i.test(n[0].nodeName),i = r && e.ui.hasScroll(n[0], "left") ? 0 : l.sizeDiff.height,s = r ? 0 : l.sizeDiff.width,o ={ width: l.helper.width() - s,height: l.helper.height() - i},u = parseInt(l.element.css("left"), 10) + (l.position.left - l.originalPosition.left) || null,a = parseInt(l.element.css("top"), 10) + (l.position.top - l.originalPosition.top) || null,f.animate || this.element.css(e.extend(o,{ top: a,left: u})),l.helper.height(l.size.height),l.helper.width(l.size.width),this._helper && !f.animate && this._proportionallyResize()),e("body").css("cursor", "auto"),this.element.removeClass("ui-resizable-resizing"),this._propagate("stop", t),this._helper && this.helper.remove(),!1},_updateVirtualBoundaries: function(e){ var t, n, i, s, o, u = this.options; o ={ minWidth: r(u.minWidth) ? u.minWidth : 0,maxWidth: r(u.maxWidth) ? u.maxWidth : Infinity,minHeight: r(u.minHeight) ? u.minHeight : 0,maxHeight: r(u.maxHeight) ? u.maxHeight : Infinity}; if (this._aspectRatio || e) t = o.minHeight * this.aspectRatio,i = o.minWidth / this.aspectRatio,n = o.maxHeight * this.aspectRatio,s = o.maxWidth / this.aspectRatio,t > o.minWidth && (o.minWidth = t),i > o.minHeight && (o.minHeight = i),n < o.maxWidth && (o.maxWidth = n),s < o.maxHeight && (o.maxHeight = s); this._vBoundaries = o},_updateCache: function(e){ this.offset = this.helper.offset(),r(e.left) && (this.position.left = e.left),r(e.top) && (this.position.top = e.top),r(e.height) && (this.size.height = e.height),r(e.width) && (this.size.width = e.width)},_updateRatio: function(e){ var t = this.position, n = this.size, i = this.axis; return r(e.height) ? e.width = e.height * this.aspectRatio : r(e.width) && (e.height = e.width / this.aspectRatio),i === "sw" && (e.left = t.left + (n.width - e.width),e.top = null),i === "nw" && (e.top = t.top + (n.height - e.height),e.left = t.left + (n.width - e.width)),e},_respectSize: function(e){ var t = this._vBoundaries, n = this.axis, i = r(e.width) && t.maxWidth && t.maxWidth < e.width, s = r(e.height) && t.maxHeight && t.maxHeight < e.height, o = r(e.width) && t.minWidth && t.minWidth > e.width, u = r(e.height) && t.minHeight && t.minHeight > e.height, a = this.originalPosition.left + this.originalSize.width, f = this.position.top + this.size.height, l =/ sw | nw | w /.test(n), c =/ nw | ne | n /.test(n); return o && (e.width = t.minWidth),u && (e.height = t.minHeight),i && (e.width = t.maxWidth),s && (e.height = t.maxHeight),o && l && (e.left = a - t.minWidth),i && l && (e.left = a - t.maxWidth),u && c && (e.top = f - t.minHeight),s && c && (e.top = f - t.maxHeight),!e.width && !e.height && !e.left && e.top ? e.top = null : !e.width && !e.height && !e.top && e.left && (e.left = null),e},_proportionallyResize: function(){ if (!this._proportionallyResizeElements.length) return; var e, t, n, r, i, s = this.helper || this.element; for (e = 0; e < this._proportionallyResizeElements.length; e++) { i = this._proportionallyResizeElements[e]; if (!this.borderDif) { this.borderDif =[],n =[i.css("borderTopWidth"), i.css("borderRightWidth"), i.css("borderBottomWidth"), i.css("borderLeftWidth")],r =[i.css("paddingTop"), i.css("paddingRight"), i.css("paddingBottom"), i.css("paddingLeft")]; for (t = 0; t < n.length; t++) this.borderDif[t] = (parseInt(n[t], 10) || 0) + (parseInt(r[t], 10) || 0)} i.css({ height: s.height() - this.borderDif[0] - this.borderDif[2] || 0,width: s.width() - this.borderDif[1] - this.borderDif[3] || 0})} },_renderProxy:function() { var t = this.element, n = this.options; this.elementOffset = t.offset(),this._helper ? (this.helper = this.helper || e("<div style='overflow:hidden;'></div>"),this.helper.addClass(this._helper).css({ width: this.element.outerWidth() - 1,height: this.element.outerHeight() - 1,position: "absolute",left: this.elementOffset.left + "px",top: this.elementOffset.top + "px",zIndex: ++n.zIndex}),this.helper.appendTo("body").disableSelection()):this.helper = this.element},_change:{e:function(e, t) { return{ width: this.originalSize.width + t} },w:function(e, t) { var n = this.originalSize, r = this.originalPosition; return{ left: r.left + t,width: n.width - t} },n:function(e, t, n) { var r = this.originalSize, i = this.originalPosition; return{ top: i.top + n,height: r.height - n} },s:function(e, t, n) { return{ height: this.originalSize.height + n} },se:function(t, n, r) { return e.extend(this._change.s.apply(this, arguments), this._change.e.apply(this,[t, n, r]))},sw:function(t, n, r) { return e.extend(this._change.s.apply(this, arguments), this._change.w.apply(this,[t, n, r]))},ne:function(t, n, r) { return e.extend(this._change.n.apply(this, arguments), this._change.e.apply(this,[t, n, r]))},nw:function(t, n, r) { return e.extend(this._change.n.apply(this, arguments), this._change.w.apply(this,[t, n, r]))}},_propagate:function(t, n) { e.ui.plugin.call(this, t,[n, this.ui()]),t !== "resize" && this._trigger(t, n, this.ui())},plugins:{},ui:function() { return{ originalElement: this.originalElement,element: this.element,helper: this.helper,position: this.position,size: this.size,originalSize: this.originalSize,originalPosition: this.originalPosition} }}),e.ui.plugin.add("resizable","animate",{stop:function(t) { var n = e(this).data("ui-resizable"), r = n.options, i = n._proportionallyResizeElements, s = i.length &&/ textarea / i.test(i[0].nodeName), o = s && e.ui.hasScroll(i[0], "left") ? 0 : n.sizeDiff.height, u = s ? 0 : n.sizeDiff.width, a = { width:n.size.width - u,height: n.size.height - o},f=parseInt(n.element.css("left"),10)+(n.position.left-n.originalPosition.left)||null,l=parseInt(n.element.css("top"),10)+(n.position.top-n.originalPosition.top)||null;n.element.animate(e.extend(a,l&&f?{top:l,left:f}:{}),{duration:r.animateDuration,easing:r.animateEasing,step:function() { var r = { width:parseInt(n.element.css("width"), 10),height: parseInt(n.element.css("height"), 10),top: parseInt(n.element.css("top"), 10),left: parseInt(n.element.css("left"), 10)}; i&&i.length&&e(i[0]).css({ width: r.width,height: r.height}),n._updateCache(r),n._propagate("resize",t)}})}}),e.ui.plugin.add("resizable","containment",{start:function() { var t, r, i, s, o, u, a, f = e(this).data("ui-resizable"), l = f.options, c = f.element, h = l.containment, p = h instanceof e?h.get(0):/ parent /.test(h) ? c.parent().get(0) : h; if (!p) return; f.containerElement = e(p),/ document /.test(h) || h === document ? (f.containerOffset ={ left: 0,top: 0},f.containerPosition ={ left: 0,top: 0},f.parentData ={ element: e(document),left: 0,top: 0,width: e(document).width(),height: e(document).height() || document.body.parentNode.scrollHeight}):(t = e(p),r =[],e(["Top", "Right", "Left", "Bottom"]).each(function(e, i){ r[e] = n(t.css("padding" + i))}),f.containerOffset = t.offset(),f.containerPosition = t.position(),f.containerSize ={ height: t.innerHeight() - r[3],width: t.innerWidth() - r[1]},i = f.containerOffset,s = f.containerSize.height,o = f.containerSize.width,u = e.ui.hasScroll(p, "left") ? p.scrollWidth : o,a = e.ui.hasScroll(p) ? p.scrollHeight : s,f.parentData ={ element: p,left: i.left,top: i.top,width: u,height: a})},resize:function(t) { var n, r, i, s, o = e(this).data("ui-resizable"), u = o.options, a = o.containerOffset, f = o.position, l = o._aspectRatio || t.shiftKey, c = { top:0,left: 0},h=o.containerElement;h[0]!==document&&/static/.test(h.css("position"))&&(c=a),f.left<(o._helper? a.left:0)&&(o.size.width=o.size.width+(o._helper? o.position.left-a.left:o.position.left-c.left),l&&(o.size.height=o.size.width/o.aspectRatio),o.position.left=u.helper? a.left:0),f.top<(o._helper? a.top:0)&&(o.size.height=o.size.height+(o._helper? o.position.top-a.top:o.position.top),l&&(o.size.width=o.size.height* o.aspectRatio),o.position.top=o._helper? a.top:0),o.offset.left=o.parentData.left+o.position.left,o.offset.top=o.parentData.top+o.position.top,n=Math.abs((o._helper? o.offset.left-c.left:o.offset.left-c.left)+o.sizeDiff.width),r=Math.abs((o._helper? o.offset.top-c.top:o.offset.top-a.top)+o.sizeDiff.height),i=o.containerElement.get(0)===o.element.parent().get(0),s=/relative|absolute/.test(o.containerElement.css("position")),i&&s&&(n-=o.parentData.left),n+o.size.width>=o.parentData.width&&(o.size.width=o.parentData.width-n,l&&(o.size.height=o.size.width/o.aspectRatio)),r+o.size.height>=o.parentData.height&&(o.size.height=o.parentData.height-r,l&&(o.size.width=o.size.height* o.aspectRatio))},stop:function() { var t = e(this).data("ui-resizable"), n = t.options, r = t.containerOffset, i = t.containerPosition, s = t.containerElement, o = e(t.helper), u = o.offset(), a = o.outerWidth() - t.sizeDiff.width, f = o.outerHeight() - t.sizeDiff.height; t._helper && !n.animate &&/ relative /.test(s.css("position")) && e(this).css({ left: u.left - i.left - r.left,width: a,height: f}),t._helper && !n.animate &&/static/.test(s.css("position")) && e(this).css({ left: u.left - i.left - r.left,width: a,height: f})}}),e.ui.plugin.add("resizable","alsoResize",{start:function() { var t = e(this).data("ui-resizable"), n = t.options, r = function(t){ e(t).each(function(){ var t = e(this); t.data("ui-resizable-alsoresize",{ width: parseInt(t.width(), 10),height: parseInt(t.height(), 10),left: parseInt(t.css("left"), 10),top: parseInt(t.css("top"), 10)})})}; typeof n.alsoResize== "object" && !n.alsoResize.parentNode ? n.alsoResize.length ? (n.alsoResize = n.alsoResize[0],r(n.alsoResize)):e.each(n.alsoResize, function(e){ r(e)}):r(n.alsoResize)},resize:function(t, n) { var r = e(this).data("ui-resizable"), i = r.options, s = r.originalSize, o = r.originalPosition, u = { height:r.size.height - s.height || 0,width: r.size.width - s.width || 0,top: r.position.top - o.top || 0,left: r.position.left - o.left || 0},a=function(t, r) { e(t).each(function(){ var t = e(this), i = e(this).data("ui-resizable-alsoresize"), s = { }, o = r && r.length ? r : t.parents(n.originalElement[0]).length?["width", "height"]:["width","height","top","left"];e.each(o,function(e, t) { var n = (i[t] || 0) + (u[t] || 0); n && n >= 0 && (s[t] = n || null)}),t.css(s)})};typeof i.alsoResize=="object"&&!i.alsoResize.nodeType? e.each(i.alsoResize, function(e, t) { a(e, t)}):a(i.alsoResize)},stop:function() { e(this).removeData("resizable-alsoresize")}}),e.ui.plugin.add("resizable","ghost",{start:function() { var t = e(this).data("ui-resizable"), n = t.options, r = t.size; t.ghost = t.originalElement.clone(),t.ghost.css({ opacity: .25,display: "block",position: "relative",height: r.height,width: r.width,margin: 0,left: 0,top: 0}).addClass("ui-resizable-ghost").addClass(typeof n.ghost== "string" ? n.ghost : ""),t.ghost.appendTo(t.helper)},resize:function() { var t = e(this).data("ui-resizable"); t.ghost && t.ghost.css({ position: "relative",height: t.size.height,width: t.size.width})},stop:function() { var t = e(this).data("ui-resizable"); t.ghost && t.helper && t.helper.get(0).removeChild(t.ghost.get(0))}}),e.ui.plugin.add("resizable","grid",{resize:function() { var t = e(this).data("ui-resizable"), n = t.options, r = t.size, i = t.originalSize, s = t.originalPosition, o = t.axis, u = typeof n.grid== "number"?[n.grid, n.grid]:n.grid,a = u[0] || 1,f = u[1] || 1,l = Math.round((r.width - i.width) / a) * a,c = Math.round((r.height - i.height) / f) * f,h = i.width + l,p = i.height + c,d = n.maxWidth && n.maxWidth < h,v = n.maxHeight && n.maxHeight < p,m = n.minWidth && n.minWidth > h,g = n.minHeight && n.minHeight > p; n.grid = u,m && (h += a),g && (p += f),d && (h -= a),v && (p -= f),/^ (se | s | e)$/.test(o) ? (t.size.width = h,t.size.height = p):/^ (ne)$/.test(o) ? (t.size.width = h,t.size.height = p,t.position.top = s.top - c):/^ (sw)$/.test(o) ? (t.size.width = h,t.size.height = p,t.position.left = s.left - l):(t.size.width = h,t.size.height = p,t.position.top = s.top - c,t.position.left = s.left - l)}})})(jQuery);(function(e, t) { var n, r, i, s, o = "ui-button ui-widget ui-state-default ui-corner-all", u = "ui-state-hover ui-state-active ", a = "ui-button-icons-only ui-button-icon-only ui-button-text-icons ui-button-text-icon-primary ui-button-text-icon-secondary ui-button-text-only", f = function(){ var t = e(this).find(":ui-button"); setTimeout(function(){ t.button("refresh")},1)},l = function(t){ var n = t.name, r = t.form, i = e([]); return n && (n = n.replace(/ '/g,"\\'"),r?i=e(r).find("[name = '"+n+"']"):i=e("[name = '"+n+"']",t.ownerDocument).filter(function(){return!this.form})),i};e.widget("ui.button",{version:"1.10.1",defaultElement:" < button > ",options:{disabled:null,text:!0,label:null,icons:{primary:null,secondary:null}},_create:function(){this.element.closest("form").unbind("reset"+this.eventNamespace).bind("reset"+this.eventNamespace,f),typeof this.options.disabled!="boolean"?this.options.disabled=!!this.element.prop("disabled"):this.element.prop("disabled",this.options.disabled),this._determineButtonType(),this.hasTitle=!!this.buttonElement.attr("title");var t=this,u=this.options,a=this.type==="checkbox"||this.type==="radio",c=a?"":"ui - state - active",h="ui - state - focus";u.label===null&&(u.label=this.type==="input"?this.buttonElement.val():this.buttonElement.html()),this._hoverable(this.buttonElement),this.buttonElement.addClass(o).attr("role","button").bind("mouseenter"+this.eventNamespace,function(){if(u.disabled)return;this===n&&e(this).addClass("ui - state - active")}).bind("mouseleave"+this.eventNamespace,function(){if(u.disabled)return;e(this).removeClass(c)}).bind("click"+this.eventNamespace,function(e){u.disabled&&(e.preventDefault(),e.stopImmediatePropagation())}),this.element.bind("focus"+this.eventNamespace,function(){t.buttonElement.addClass(h)}).bind("blur"+this.eventNamespace,function(){t.buttonElement.removeClass(h)}),a&&(this.element.bind("change"+this.eventNamespace,function(){if(s)return;t.refresh()}),this.buttonElement.bind("mousedown"+this.eventNamespace,function(e){if(u.disabled)return;s=!1,r=e.pageX,i=e.pageY}).bind("mouseup"+this.eventNamespace,function(e){if(u.disabled)return;if(r!==e.pageX||i!==e.pageY)s=!0})),this.type==="checkbox"?this.buttonElement.bind("click"+this.eventNamespace,function(){if(u.disabled||s)return!1}):this.type==="radio"?this.buttonElement.bind("click"+this.eventNamespace,function(){if(u.disabled||s)return!1;e(this).addClass("ui - state - active"),t.buttonElement.attr("aria - pressed","true");var n=t.element[0];l(n).not(n).map(function(){return e(this).button("widget")[0]}).removeClass("ui - state - active").attr("aria - pressed","false")}):(this.buttonElement.bind("mousedown"+this.eventNamespace,function(){if(u.disabled)return!1;e(this).addClass("ui - state - active"),n=this,t.document.one("mouseup",function(){n=null})}).bind("mouseup"+this.eventNamespace,function(){if(u.disabled)return!1;e(this).removeClass("ui - state - active")}).bind("keydown"+this.eventNamespace,function(t){if(u.disabled)return!1;(t.keyCode===e.ui.keyCode.SPACE||t.keyCode===e.ui.keyCode.ENTER)&&e(this).addClass("ui - state - active")}).bind("keyup"+this.eventNamespace+" blur"+this.eventNamespace,function(){e(this).removeClass("ui - state - active")}),this.buttonElement.is("a")&&this.buttonElement.keyup(function(t){t.keyCode===e.ui.keyCode.SPACE&&e(this).click()})),this._setOption("disabled",u.disabled),this._resetButton()},_determineButtonType:function(){var e,t,n;this.element.is("[type = checkbox]")?this.type="checkbox":this.element.is("[type = radio]")?this.type="radio":this.element.is("input")?this.type="input":this.type="button",this.type==="checkbox"||this.type==="radio"?(e=this.element.parents().last(),t="label[for= '"+this.element.attr("id")+"']",this.buttonElement=e.find(t),this.buttonElement.length||(e=e.length?e.siblings():this.element.siblings(),this.buttonElement=e.filter(t),this.buttonElement.length||(this.buttonElement=e.find(t))),this.element.addClass("ui - helper - hidden - accessible"),n=this.element.is(":checked"),n&&this.buttonElement.addClass("ui - state - active"),this.buttonElement.prop("aria - pressed",n)):this.buttonElement=this.element},widget:function(){return this.buttonElement},_destroy:function(){this.element.removeClass("ui - helper - hidden - accessible"),this.buttonElement.removeClass(o+" "+u+" "+a).removeAttr("role").removeAttr("aria - pressed").html(this.buttonElement.find(".ui - button - text").html()),this.hasTitle||this.buttonElement.removeAttr("title")},_setOption:function(e,t){this._super(e,t);if(e==="disabled"){t?this.element.prop("disabled",!0):this.element.prop("disabled",!1);return}this._resetButton()},refresh:function(){var t=this.element.is("input, button")?this.element.is(":disabled"):this.element.hasClass("ui - button - disabled");t!==this.options.disabled&&this._setOption("disabled",t),this.type==="radio"?l(this.element[0]).each(function(){e(this).is(":checked")?e(this).button("widget").addClass("ui - state - active").attr("aria - pressed","true"):e(this).button("widget").removeClass("ui - state - active").attr("aria - pressed","false")}):this.type==="checkbox"&&(this.element.is(":checked")?this.buttonElement.addClass("ui - state - active").attr("aria - pressed","true"):this.buttonElement.removeClass("ui - state - active").attr("aria - pressed","false"))},_resetButton:function(){if(this.type==="input"){this.options.label&&this.element.val(this.options.label);return}var t=this.buttonElement.removeClass(a),n=e(" < span ></ span > ",this.document[0]).addClass("ui - button - text").html(this.options.label).appendTo(t.empty()).text(),r=this.options.icons,i=r.primary&&r.secondary,s=[];r.primary||r.secondary?(this.options.text&&s.push("ui - button - text - icon"+(i?"s":r.primary?" - primary":" - secondary")),r.primary&&t.prepend(" < span class='ui-button-icon-primary ui-icon "+r.primary+"'></span>"),r.secondary&&t.append("<span class='ui-button-icon-secondary ui-icon "+r.secondary+"'></span>"),this.options.text||(s.push(i?"ui-button-icons-only":"ui-button-icon-only"),this.hasTitle||t.attr("title",e.trim(n)))):s.push("ui-button-text-only"),t.addClass(s.join(" "))}}),e.widget("ui.buttonset",{version:"1.10.1",options:{items:"button, input[type = button], input[type = submit], input[type = reset], input[type = checkbox], input[type = radio], a, :data(ui-button)"},_create:function(){this.element.addClass("ui-buttonset")},_init:function(){this.refresh()},_setOption:function(e,t){e==="disabled"&&this.buttons.button("option",e,t),this._super(e,t)},refresh:function(){var t=this.element.css("direction")==="rtl";this.buttons=this.element.find(this.options.items).filter(":ui-button").button("refresh").end().not(":ui-button").button().end().map(function(){return e(this).button("widget")[0]}).removeClass("ui-corner-all ui-corner-left ui-corner-right").filter(":first").addClass(t?"ui-corner-right":"ui-corner-left").end().filter(":last").addClass(t?"ui-corner-left":"ui-corner-right").end().end()},_destroy:function(){this.element.removeClass("ui-buttonset"),this.buttons.map(function(){return e(this).button("widget")[0]}).removeClass("ui-corner-left ui-corner-right").end().button("destroy")}})})(jQuery);(function(e,t){function s(){this._curInst=null,this._keyEvent=!1,this._disabledInputs=[],this._datepickerShowing=!1,this._inDialog=!1,this._mainDivId="ui-datepicker-div",this._inlineClass="ui-datepicker-inline",this._appendClass="ui-datepicker-append",this._triggerClass="ui-datepicker-trigger",this._dialogClass="ui-datepicker-dialog",this._disableClass="ui-datepicker-disabled",this._unselectableClass="ui-datepicker-unselectable",this._currentClass="ui-datepicker-current-day",this._dayOverClass="ui-datepicker-days-cell-over",this.regional=[],this.regional[""]={closeText:"Done",prevText:"Prev",nextText:"Next",currentText:"Today",monthNames:["January","February","March","April","May","June","July","August","September","October","November","December"],monthNamesShort:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],dayNames:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"],dayNamesShort:["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],dayNamesMin:["Su","Mo","Tu","We","Th","Fr","Sa"],weekHeader:"Wk",dateFormat:"mm/dd/yy",firstDay:0,isRTL:!1,showMonthAfterYear:!1,yearSuffix:""},this._defaults={showOn:"focus",showAnim:"fadeIn",showOptions:{},defaultDate:null,appendText:"",buttonText:"...",buttonImage:"",buttonImageOnly:!1,hideIfNoPrevNext:!1,navigationAsDateFormat:!1,gotoCurrent:!1,changeMonth:!1,changeYear:!1,yearRange:"c-10:c+10",showOtherMonths:!1,selectOtherMonths:!1,showWeek:!1,calculateWeek:this.iso8601Week,shortYearCutoff:"+10",minDate:null,maxDate:null,duration:"fast",beforeShowDay:null,beforeShow:null,onSelect:null,onChangeMonthYear:null,onClose:null,numberOfMonths:1,showCurrentAtPos:0,stepMonths:1,stepBigMonths:12,altField:"",altFormat:"",constrainInput:!0,showButtonPanel:!1,autoSize:!1,disabled:!1},e.extend(this._defaults,this.regional[""]),this.dpDiv=o(e("<div id = '"+this._mainDivId+"' class='ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all'></div>"))}function o(t){var n="button, .ui-datepicker-prev, .ui-datepicker-next, .ui-datepicker-calendar td a";return t.delegate(n,"mouseout",function(){e(this).removeClass("ui-state-hover"),this.className.indexOf("ui-datepicker-prev")!==-1&&e(this).removeClass("ui-datepicker-prev-hover"),this.className.indexOf("ui-datepicker-next")!==-1&&e(this).removeClass("ui-datepicker-next-hover")}).delegate(n,"mouseover",function(){e.datepicker._isDisabledDatepicker(i.inline?t.parent()[0]:i.input[0])||(e(this).parents(".ui-datepicker-calendar").find("a").removeClass("ui-state-hover"),e(this).addClass("ui-state-hover"),this.className.indexOf("ui-datepicker-prev")!==-1&&e(this).addClass("ui-datepicker-prev-hover"),this.className.indexOf("ui-datepicker-next")!==-1&&e(this).addClass("ui-datepicker-next-hover"))})}function u(t,n){e.extend(t,n);for(var r in n)n[r]==null&&(t[r]=n[r]);return t}e.extend(e.ui,{datepicker:{version:"1.10.1"}});var n="datepicker",r=(new Date).getTime(),i;e.extend(s.prototype,{markerClassName:"hasDatepicker",maxRows:4,_widgetDatepicker:function(){return this.dpDiv},setDefaults:function(e){return u(this._defaults,e||{}),this},_attachDatepicker:function(t,n){var r,i,s;r=t.nodeName.toLowerCase(),i=r==="div"||r==="span",t.id||(this.uuid+=1,t.id="dp"+this.uuid),s=this._newInst(e(t),i),s.settings=e.extend({},n||{}),r==="input"?this._connectDatepicker(t,s):i&&this._inlineDatepicker(t,s)},_newInst:function(t,n){var r=t[0].id.replace(/([^A-Za-z0-9_\-])/g,"\\\\$1");return{id:r,input:t,selectedDay:0,selectedMonth:0,selectedYear:0,drawMonth:0,drawYear:0,inline:n,dpDiv:n?o(e("<div class='"+this._inlineClass+" ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all'></div>")):this.dpDiv}},_connectDatepicker:function(t,r){var i=e(t);r.append=e([]),r.trigger=e([]);if(i.hasClass(this.markerClassName))return;this._attachments(i,r),i.addClass(this.markerClassName).keydown(this._doKeyDown).keypress(this._doKeyPress).keyup(this._doKeyUp),this._autoSize(r),e.data(t,n,r),r.settings.disabled&&this._disableDatepicker(t)},_attachments:function(t,n){var r,i,s,o=this._get(n,"appendText"),u=this._get(n,"isRTL");n.append&&n.append.remove(),o&&(n.append=e("<span class='"+this._appendClass+"'>"+o+"</span>"),t[u?"before":"after"](n.append)),t.unbind("focus",this._showDatepicker),n.trigger&&n.trigger.remove(),r=this._get(n,"showOn"),(r==="focus"||r==="both")&&t.focus(this._showDatepicker);if(r==="button"||r==="both")i=this._get(n,"buttonText"),s=this._get(n,"buttonImage"),n.trigger=e(this._get(n,"buttonImageOnly")?e("<img/>").addClass(this._triggerClass).attr({src:s,alt:i,title:i}):e("<button type = 'button' ></ button > ").addClass(this._triggerClass).html(s?e(" < img /> ").attr({src:s,alt:i,title:i}):i)),t[u?"before":"after"](n.trigger),n.trigger.click(function(){return e.datepicker._datepickerShowing&&e.datepicker._lastInput===t[0]?e.datepicker._hideDatepicker():e.datepicker._datepickerShowing&&e.datepicker._lastInput!==t[0]?(e.datepicker._hideDatepicker(),e.datepicker._showDatepicker(t[0])):e.datepicker._showDatepicker(t[0]),!1})},_autoSize:function(e){if(this._get(e,"autoSize")&&!e.inline){var t,n,r,i,s=new Date(2009,11,20),o=this._get(e,"dateFormat");o.match(/[DM]/)&&(t=function(e){n=0,r=0;for(i=0;i<e.length;i++)e[i].length>n&&(n=e[i].length,r=i);return r},s.setMonth(t(this._get(e,o.match(/MM/)?"monthNames":"monthNamesShort"))),s.setDate(t(this._get(e,o.match(/DD/)?"dayNames":"dayNamesShort"))+20-s.getDay())),e.input.attr("size",this._formatDate(e,s).length)}},_inlineDatepicker:function(t,r){var i=e(t);if(i.hasClass(this.markerClassName))return;i.addClass(this.markerClassName).append(r.dpDiv),e.data(t,n,r),this._setDate(r,this._getDefaultDate(r),!0),this._updateDatepicker(r),this._updateAlternate(r),r.settings.disabled&&this._disableDatepicker(t),r.dpDiv.css("display","block")},_dialogDatepicker:function(t,r,i,s,o){var a,f,l,c,h,p=this._dialogInst;return p||(this.uuid+=1,a="dp"+this.uuid,this._dialogInput=e("<input type = 'text' id='"+a+"' style='position: absolute; top: -100px; width: 0px;'/>"),this._dialogInput.keydown(this._doKeyDown),e("body").append(this._dialogInput),p=this._dialogInst=this._newInst(this._dialogInput,!1),p.settings={},e.data(this._dialogInput[0],n,p)),u(p.settings,s||{}),r=r&&r.constructor===Date?this._formatDate(p,r):r,this._dialogInput.val(r),this._pos=o?o.length?o:[o.pageX,o.pageY]:null,this._pos||(f=document.documentElement.clientWidth,l=document.documentElement.clientHeight,c=document.documentElement.scrollLeft||document.body.scrollLeft,h=document.documentElement.scrollTop||document.body.scrollTop,this._pos=[f/2-100+c,l/2-150+h]),this._dialogInput.css("left",this._pos[0]+20+"px").css("top",this._pos[1]+"px"),p.settings.onSelect=i,this._inDialog=!0,this.dpDiv.addClass(this._dialogClass),this._showDatepicker(this._dialogInput[0]),e.blockUI&&e.blockUI(this.dpDiv),e.data(this._dialogInput[0],n,p),this},_destroyDatepicker:function(t){var r,i=e(t),s=e.data(t,n);if(!i.hasClass(this.markerClassName))return;r=t.nodeName.toLowerCase(),e.removeData(t,n),r==="input"?(s.append.remove(),s.trigger.remove(),i.removeClass(this.markerClassName).unbind("focus",this._showDatepicker).unbind("keydown",this._doKeyDown).unbind("keypress",this._doKeyPress).unbind("keyup",this._doKeyUp)):(r==="div"||r==="span")&&i.removeClass(this.markerClassName).empty()},_enableDatepicker:function(t){var r,i,s=e(t),o=e.data(t,n);if(!s.hasClass(this.markerClassName))return;r=t.nodeName.toLowerCase();if(r==="input")t.disabled=!1,o.trigger.filter("button").each(function(){this.disabled=!1}).end().filter("img").css({opacity:"1.0",cursor:""});else if(r==="div"||r==="span")i=s.children("."+this._inlineClass),i.children().removeClass("ui-state-disabled"),i.find("select.ui-datepicker-month, select.ui-datepicker-year").prop("disabled",!1);this._disabledInputs=e.map(this._disabledInputs,function(e){return e===t?null:e})},_disableDatepicker:function(t){var r,i,s=e(t),o=e.data(t,n);if(!s.hasClass(this.markerClassName))return;r=t.nodeName.toLowerCase();if(r==="input")t.disabled=!0,o.trigger.filter("button").each(function(){this.disabled=!0}).end().filter("img").css({opacity:"0.5",cursor:"default"});else if(r==="div"||r==="span")i=s.children("."+this._inlineClass),i.children().addClass("ui-state-disabled"),i.find("select.ui-datepicker-month, select.ui-datepicker-year").prop("disabled",!0);this._disabledInputs=e.map(this._disabledInputs,function(e){return e===t?null:e}),this._disabledInputs[this._disabledInputs.length]=t},_isDisabledDatepicker:function(e){if(!e)return!1;for(var t=0;t<this._disabledInputs.length;t++)if(this._disabledInputs[t]===e)return!0;return!1},_getInst:function(t){try{return e.data(t,n)}catch(r){throw"Missing instance data for this datepicker"}},_optionDatepicker:function(n,r,i){var s,o,a,f,l=this._getInst(n);if(arguments.length===2&&typeof r=="string")return r==="defaults"?e.extend({},e.datepicker._defaults):l?r==="all"?e.extend({},l.settings):this._get(l,r):null;s=r||{},typeof r=="string"&&(s={},s[r]=i),l&&(this._curInst===l&&this._hideDatepicker(),o=this._getDateDatepicker(n,!0),a=this._getMinMaxDate(l,"min"),f=this._getMinMaxDate(l,"max"),u(l.settings,s),a!==null&&s.dateFormat!==t&&s.minDate===t&&(l.settings.minDate=this._formatDate(l,a)),f!==null&&s.dateFormat!==t&&s.maxDate===t&&(l.settings.maxDate=this._formatDate(l,f)),"disabled"in s&&(s.disabled?this._disableDatepicker(n):this._enableDatepicker(n)),this._attachments(e(n),l),this._autoSize(l),this._setDate(l,o),this._updateAlternate(l),this._updateDatepicker(l))},_changeDatepicker:function(e,t,n){this._optionDatepicker(e,t,n)},_refreshDatepicker:function(e){var t=this._getInst(e);t&&this._updateDatepicker(t)},_setDateDatepicker:function(e,t){var n=this._getInst(e);n&&(this._setDate(n,t),this._updateDatepicker(n),this._updateAlternate(n))},_getDateDatepicker:function(e,t){var n=this._getInst(e);return n&&!n.inline&&this._setDateFromField(n,t),n?this._getDate(n):null},_doKeyDown:function(t){var n,r,i,s=e.datepicker._getInst(t.target),o=!0,u=s.dpDiv.is(".ui-datepicker-rtl");s._keyEvent=!0;if(e.datepicker._datepickerShowing)switch(t.keyCode){case 9:e.datepicker._hideDatepicker(),o=!1;break;case 13:return i=e("td."+e.datepicker._dayOverClass+":not(."+e.datepicker._currentClass+")",s.dpDiv),i[0]&&e.datepicker._selectDay(t.target,s.selectedMonth,s.selectedYear,i[0]),n=e.datepicker._get(s,"onSelect"),n?(r=e.datepicker._formatDate(s),n.apply(s.input?s.input[0]:null,[r,s])):e.datepicker._hideDatepicker(),!1;case 27:e.datepicker._hideDatepicker();break;case 33:e.datepicker._adjustDate(t.target,t.ctrlKey?-e.datepicker._get(s,"stepBigMonths"):-e.datepicker._get(s,"stepMonths"),"M");break;case 34:e.datepicker._adjustDate(t.target,t.ctrlKey?+e.datepicker._get(s,"stepBigMonths"):+e.datepicker._get(s,"stepMonths"),"M");break;case 35:(t.ctrlKey||t.metaKey)&&e.datepicker._clearDate(t.target),o=t.ctrlKey||t.metaKey;break;case 36:(t.ctrlKey||t.metaKey)&&e.datepicker._gotoToday(t.target),o=t.ctrlKey||t.metaKey;break;case 37:(t.ctrlKey||t.metaKey)&&e.datepicker._adjustDate(t.target,u?1:-1,"D"),o=t.ctrlKey||t.metaKey,t.originalEvent.altKey&&e.datepicker._adjustDate(t.target,t.ctrlKey?-e.datepicker._get(s,"stepBigMonths"):-e.datepicker._get(s,"stepMonths"),"M");break;case 38:(t.ctrlKey||t.metaKey)&&e.datepicker._adjustDate(t.target,-7,"D"),o=t.ctrlKey||t.metaKey;break;case 39:(t.ctrlKey||t.metaKey)&&e.datepicker._adjustDate(t.target,u?-1:1,"D"),o=t.ctrlKey||t.metaKey,t.originalEvent.altKey&&e.datepicker._adjustDate(t.target,t.ctrlKey?+e.datepicker._get(s,"stepBigMonths"):+e.datepicker._get(s,"stepMonths"),"M");break;case 40:(t.ctrlKey||t.metaKey)&&e.datepicker._adjustDate(t.target,7,"D"),o=t.ctrlKey||t.metaKey;break;default:o=!1}else t.keyCode===36&&t.ctrlKey?e.datepicker._showDatepicker(this):o=!1;o&&(t.preventDefault(),t.stopPropagation())},_doKeyPress:function(t){var n,r,i=e.datepicker._getInst(t.target);if(e.datepicker._get(i,"constrainInput"))return n=e.datepicker._possibleChars(e.datepicker._get(i,"dateFormat")),r=String.fromCharCode(t.charCode==null?t.keyCode:t.charCode),t.ctrlKey||t.metaKey||r<" "||!n||n.indexOf(r)>-1},_doKeyUp:function(t){var n,r=e.datepicker._getInst(t.target);if(r.input.val()!==r.lastVal)try{n=e.datepicker.parseDate(e.datepicker._get(r,"dateFormat"),r.input?r.input.val():null,e.datepicker._getFormatConfig(r)),n&&(e.datepicker._setDateFromField(r),e.datepicker._updateAlternate(r),e.datepicker._updateDatepicker(r))}catch(i){}return!0},_showDatepicker:function(t){t=t.target||t,t.nodeName.toLowerCase()!=="input"&&(t=e("input",t.parentNode)[0]);if(e.datepicker._isDisabledDatepicker(t)||e.datepicker._lastInput===t)return;var n,r,i,s,o,a,f;n=e.datepicker._getInst(t),e.datepicker._curInst&&e.datepicker._curInst!==n&&(e.datepicker._curInst.dpDiv.stop(!0,!0),n&&e.datepicker._datepickerShowing&&e.datepicker._hideDatepicker(e.datepicker._curInst.input[0])),r=e.datepicker._get(n,"beforeShow"),i=r?r.apply(t,[t,n]):{};if(i===!1)return;u(n.settings,i),n.lastVal=null,e.datepicker._lastInput=t,e.datepicker._setDateFromField(n),e.datepicker._inDialog&&(t.value=""),e.datepicker._pos||(e.datepicker._pos=e.datepicker._findPos(t),e.datepicker._pos[1]+=t.offsetHeight),s=!1,e(t).parents().each(function(){return s|=e(this).css("position")==="fixed",!s}),o={left:e.datepicker._pos[0],top:e.datepicker._pos[1]},e.datepicker._pos=null,n.dpDiv.empty(),n.dpDiv.css({position:"absolute",display:"block",top:"-1000px"}),e.datepicker._updateDatepicker(n),o=e.datepicker._checkOffset(n,o,s),n.dpDiv.css({position:e.datepicker._inDialog&&e.blockUI?"static":s?"fixed":"absolute",display:"none",left:o.left+"px",top:o.top+"px"}),n.inline||(a=e.datepicker._get(n,"showAnim"),f=e.datepicker._get(n,"duration"),n.dpDiv.zIndex(e(t).zIndex()+1),e.datepicker._datepickerShowing=!0,e.effects&&e.effects.effect[a]?n.dpDiv.show(a,e.datepicker._get(n,"showOptions"),f):n.dpDiv[a||"show"](a?f:null),n.input.is(":visible")&&!n.input.is(":disabled")&&n.input.focus(),e.datepicker._curInst=n)},_updateDatepicker:function(t){this.maxRows=4,i=t,t.dpDiv.empty().append(this._generateHTML(t)),this._attachHandlers(t),t.dpDiv.find("."+this._dayOverClass+" a").mouseover();var n,r=this._getNumberOfMonths(t),s=r[1],o=17;t.dpDiv.removeClass("ui-datepicker-multi-2 ui-datepicker-multi-3 ui-datepicker-multi-4").width(""),s>1&&t.dpDiv.addClass("ui-datepicker-multi-"+s).css("width",o*s+"em"),t.dpDiv[(r[0]!==1||r[1]!==1?"add":"remove")+"Class"]("ui-datepicker-multi"),t.dpDiv[(this._get(t,"isRTL")?"add":"remove")+"Class"]("ui-datepicker-rtl"),t===e.datepicker._curInst&&e.datepicker._datepickerShowing&&t.input&&t.input.is(":visible")&&!t.input.is(":disabled")&&t.input[0]!==document.activeElement&&t.input.focus(),t.yearshtml&&(n=t.yearshtml,setTimeout(function(){n===t.yearshtml&&t.yearshtml&&t.dpDiv.find("select.ui-datepicker-year:first").replaceWith(t.yearshtml),n=t.yearshtml=null},0))},_getBorders:function(e){var t=function(e){return{thin:1,medium:2,thick:3}[e]||e};return[parseFloat(t(e.css("border-left-width"))),parseFloat(t(e.css("border-top-width")))]},_checkOffset:function(t,n,r){var i=t.dpDiv.outerWidth(),s=t.dpDiv.outerHeight(),o=t.input?t.input.outerWidth():0,u=t.input?t.input.outerHeight():0,a=document.documentElement.clientWidth+(r?0:e(document).scrollLeft()),f=document.documentElement.clientHeight+(r?0:e(document).scrollTop());return n.left-=this._get(t,"isRTL")?i-o:0,n.left-=r&&n.left===t.input.offset().left?e(document).scrollLeft():0,n.top-=r&&n.top===t.input.offset().top+u?e(document).scrollTop():0,n.left-=Math.min(n.left,n.left+i>a&&a>i?Math.abs(n.left+i-a):0),n.top-=Math.min(n.top,n.top+s>f&&f>s?Math.abs(s+u):0),n},_findPos:function(t){var n,r=this._getInst(t),i=this._get(r,"isRTL");while(t&&(t.type==="hidden"||t.nodeType!==1||e.expr.filters.hidden(t)))t=t[i?"previousSibling":"nextSibling"];return n=e(t).offset(),[n.left,n.top]},_hideDatepicker:function(t){var r,i,s,o,u=this._curInst;if(!u||t&&u!==e.data(t,n))return;this._datepickerShowing&&(r=this._get(u,"showAnim"),i=this._get(u,"duration"),s=function(){e.datepicker._tidyDialog(u)},e.effects&&(e.effects.effect[r]||e.effects[r])?u.dpDiv.hide(r,e.datepicker._get(u,"showOptions"),i,s):u.dpDiv[r==="slideDown"?"slideUp":r==="fadeIn"?"fadeOut":"hide"](r?i:null,s),r||s(),this._datepickerShowing=!1,o=this._get(u,"onClose"),o&&o.apply(u.input?u.input[0]:null,[u.input?u.input.val():"",u]),this._lastInput=null,this._inDialog&&(this._dialogInput.css({position:"absolute",left:"0",top:"-100px"}),e.blockUI&&(e.unblockUI(),e("body").append(this.dpDiv))),this._inDialog=!1)},_tidyDialog:function(e){e.dpDiv.removeClass(this._dialogClass).unbind(".ui-datepicker-calendar")},_checkExternalClick:function(t){if(!e.datepicker._curInst)return;var n=e(t.target),r=e.datepicker._getInst(n[0]);(n[0].id!==e.datepicker._mainDivId&&n.parents("#"+e.datepicker._mainDivId).length===0&&!n.hasClass(e.datepicker.markerClassName)&&!n.closest("."+e.datepicker._triggerClass).length&&e.datepicker._datepickerShowing&&(!e.datepicker._inDialog||!e.blockUI)||n.hasClass(e.datepicker.markerClassName)&&e.datepicker._curInst!==r)&&e.datepicker._hideDatepicker()},_adjustDate:function(t,n,r){var i=e(t),s=this._getInst(i[0]);if(this._isDisabledDatepicker(i[0]))return;this._adjustInstDate(s,n+(r==="M"?this._get(s,"showCurrentAtPos"):0),r),this._updateDatepicker(s)},_gotoToday:function(t){var n,r=e(t),i=this._getInst(r[0]);this._get(i,"gotoCurrent")&&i.currentDay?(i.selectedDay=i.currentDay,i.drawMonth=i.selectedMonth=i.currentMonth,i.drawYear=i.selectedYear=i.currentYear):(n=new Date,i.selectedDay=n.getDate(),i.drawMonth=i.selectedMonth=n.getMonth(),i.drawYear=i.selectedYear=n.getFullYear()),this._notifyChange(i),this._adjustDate(r)},_selectMonthYear:function(t,n,r){var i=e(t),s=this._getInst(i[0]);s["selected"+(r==="M"?"Month":"Year")]=s["draw"+(r==="M"?"Month":"Year")]=parseInt(n.options[n.selectedIndex].value,10),this._notifyChange(s),this._adjustDate(i)},_selectDay:function(t,n,r,i){var s,o=e(t);if(e(i).hasClass(this._unselectableClass)||this._isDisabledDatepicker(o[0]))return;s=this._getInst(o[0]),s.selectedDay=s.currentDay=e("a",i).html(),s.selectedMonth=s.currentMonth=n,s.selectedYear=s.currentYear=r,this._selectDate(t,this._formatDate(s,s.currentDay,s.currentMonth,s.currentYear))},_clearDate:function(t){var n=e(t);this._selectDate(n,"")},_selectDate:function(t,n){var r,i=e(t),s=this._getInst(i[0]);n=n!=null?n:this._formatDate(s),s.input&&s.input.val(n),this._updateAlternate(s),r=this._get(s,"onSelect"),r?r.apply(s.input?s.input[0]:null,[n,s]):s.input&&s.input.trigger("change"),s.inline?this._updateDatepicker(s):(this._hideDatepicker(),this._lastInput=s.input[0],typeof s.input[0]!="object"&&s.input.focus(),this._lastInput=null)},_updateAlternate:function(t){var n,r,i,s=this._get(t,"altField");s&&(n=this._get(t,"altFormat")||this._get(t,"dateFormat"),r=this._getDate(t),i=this.formatDate(n,r,this._getFormatConfig(t)),e(s).each(function(){e(this).val(i)}))},noWeekends:function(e){var t=e.getDay();return[t>0&&t<6,""]},iso8601Week:function(e){var t,n=new Date(e.getTime());return n.setDate(n.getDate()+4-(n.getDay()||7)),t=n.getTime(),n.setMonth(0),n.setDate(1),Math.floor(Math.round((t-n)/864e5)/7)+1},parseDate:function(t,n,r){if(t==null||n==null)throw"Invalid arguments";n=typeof n=="object"?n.toString():n+"";if(n==="")return null;var i,s,o,u=0,a=(r?r.shortYearCutoff:null)||this._defaults.shortYearCutoff,f=typeof a!="string"?a:(new Date).getFullYear()%100+parseInt(a,10),l=(r?r.dayNamesShort:null)||this._defaults.dayNamesShort,c=(r?r.dayNames:null)||this._defaults.dayNames,h=(r?r.monthNamesShort:null)||this._defaults.monthNamesShort,p=(r?r.monthNames:null)||this._defaults.monthNames,d=-1,v=-1,m=-1,g=-1,y=!1,b,w=function(e){var n=i+1<t.length&&t.charAt(i+1)===e;return n&&i++,n},E=function(e){var t=w(e),r=e==="@"?14:e==="!"?20:e==="y"&&t?4:e==="o"?3:2,i=new RegExp("^\\d{1,"+r+"}"),s=n.substring(u).match(i);if(!s)throw"Missing number at position "+u;return u+=s[0].length,parseInt(s[0],10)},S=function(t,r,i){var s=-1,o=e.map(w(t)?i:r,function(e,t){return[[t,e]]}).sort(function(e,t){return-(e[1].length-t[1].length)});e.each(o,function(e,t){var r=t[1];if(n.substr(u,r.length).toLowerCase()===r.toLowerCase())return s=t[0],u+=r.length,!1});if(s!==-1)return s+1;throw"Unknown name at position "+u},x=function(){if(n.charAt(u)!==t.charAt(i))throw"Unexpected literal at position "+u;u++};for(i=0;i<t.length;i++)if(y)t.charAt(i)==="'"&&!w("'")?y=!1:x();else switch(t.charAt(i)){case"d":m=E("d");break;case"D":S("D",l,c);break;case"o":g=E("o");break;case"m":v=E("m");break;case"M":v=S("M",h,p);break;case"y":d=E("y");break;case"@":b=new Date(E("@")),d=b.getFullYear(),v=b.getMonth()+1,m=b.getDate();break;case"!":b=new Date((E("!")-this._ticksTo1970)/1e4),d=b.getFullYear(),v=b.getMonth()+1,m=b.getDate();break;case"'":w("'")?x():y=!0;break;default:x()}if(u<n.length){o=n.substr(u);if(!/^\s+/.test(o))throw"Extra/unparsed characters found in date: "+o}d===-1?d=(new Date).getFullYear():d<100&&(d+=(new Date).getFullYear()-(new Date).getFullYear()%100+(d<=f?0:-100));if(g>-1){v=1,m=g;do{s=this._getDaysInMonth(d,v-1);if(m<=s)break;v++,m-=s}while(!0)}b=this._daylightSavingAdjust(new Date(d,v-1,m));if(b.getFullYear()!==d||b.getMonth()+1!==v||b.getDate()!==m)throw"Invalid date";return b},ATOM:"yy-mm-dd",COOKIE:"D, dd M yy",ISO_8601:"yy-mm-dd",RFC_822:"D, d M y",RFC_850:"DD, dd-M-y",RFC_1036:"D, d M y",RFC_1123:"D, d M yy",RFC_2822:"D, d M yy",RSS:"D, d M y",TICKS:"!",TIMESTAMP:"@",W3C:"yy-mm-dd",_ticksTo1970:(718685+Math.floor(492.5)-Math.floor(19.7)+Math.floor(4.925))*24*60*60*1e7,formatDate:function(e,t,n){if(!t)return"";var r,i=(n?n.dayNamesShort:null)||this._defaults.dayNamesShort,s=(n?n.dayNames:null)||this._defaults.dayNames,o=(n?n.monthNamesShort:null)||this._defaults.monthNamesShort,u=(n?n.monthNames:null)||this._defaults.monthNames,a=function(t){var n=r+1<e.length&&e.charAt(r+1)===t;return n&&r++,n},f=function(e,t,n){var r=""+t;if(a(e))while(r.length<n)r="0"+r;return r},l=function(e,t,n,r){return a(e)?r[t]:n[t]},c="",h=!1;if(t)for(r=0;r<e.length;r++)if(h)e.charAt(r)==="'"&&!a("'")?h=!1:c+=e.charAt(r);else switch(e.charAt(r)){case"d":c+=f("d",t.getDate(),2);break;case"D":c+=l("D",t.getDay(),i,s);break;case"o":c+=f("o",Math.round(((new Date(t.getFullYear(),t.getMonth(),t.getDate())).getTime()-(new Date(t.getFullYear(),0,0)).getTime())/864e5),3);break;case"m":c+=f("m",t.getMonth()+1,2);break;case"M":c+=l("M",t.getMonth(),o,u);break;case"y":c+=a("y")?t.getFullYear():(t.getYear()%100<10?"0":"")+t.getYear()%100;break;case"@":c+=t.getTime();break;case"!":c+=t.getTime()*1e4+this._ticksTo1970;break;case"'":a("'")?c+="'":h=!0;break;default:c+=e.charAt(r)}return c},_possibleChars:function(e){var t,n="",r=!1,i=function(n){var r=t+1<e.length&&e.charAt(t+1)===n;return r&&t++,r};for(t=0;t<e.length;t++)if(r)e.charAt(t)==="'"&&!i("'")?r=!1:n+=e.charAt(t);else switch(e.charAt(t)){case"d":case"m":case"y":case"@":n+="0123456789";break;case"D":case"M":return null;case"'":i("'")?n+="'":r=!0;break;default:n+=e.charAt(t)}return n},_get:function(e,n){return e.settings[n]!==t?e.settings[n]:this._defaults[n]},_setDateFromField:function(e,t){if(e.input.val()===e.lastVal)return;var n=this._get(e,"dateFormat"),r=e.lastVal=e.input?e.input.val():null,i=this._getDefaultDate(e),s=i,o=this._getFormatConfig(e);try{s=this.parseDate(n,r,o)||i}catch(u){r=t?"":r}e.selectedDay=s.getDate(),e.drawMonth=e.selectedMonth=s.getMonth(),e.drawYear=e.selectedYear=s.getFullYear(),e.currentDay=r?s.getDate():0,e.currentMonth=r?s.getMonth():0,e.currentYear=r?s.getFullYear():0,this._adjustInstDate(e)},_getDefaultDate:function(e){return this._restrictMinMax(e,this._determineDate(e,this._get(e,"defaultDate"),new Date))},_determineDate:function(t,n,r){var i=function(e){var t=new Date;return t.setDate(t.getDate()+e),t},s=function(n){try{return e.datepicker.parseDate(e.datepicker._get(t,"dateFormat"),n,e.datepicker._getFormatConfig(t))}catch(r){}var i=(n.toLowerCase().match(/^c/)?e.datepicker._getDate(t):null)||new Date,s=i.getFullYear(),o=i.getMonth(),u=i.getDate(),a=/([+\-]?[0-9]+)\s*(d|D|w|W|m|M|y|Y)?/g,f=a.exec(n);while(f){switch(f[2]||"d"){case"d":case"D":u+=parseInt(f[1],10);break;case"w":case"W":u+=parseInt(f[1],10)*7;break;case"m":case"M":o+=parseInt(f[1],10),u=Math.min(u,e.datepicker._getDaysInMonth(s,o));break;case"y":case"Y":s+=parseInt(f[1],10),u=Math.min(u,e.datepicker._getDaysInMonth(s,o))}f=a.exec(n)}return new Date(s,o,u)},o=n==null||n===""?r:typeof n=="string"?s(n):typeof n=="number"?isNaN(n)?r:i(n):new Date(n.getTime());return o=o&&o.toString()==="Invalid Date"?r:o,o&&(o.setHours(0),o.setMinutes(0),o.setSeconds(0),o.setMilliseconds(0)),this._daylightSavingAdjust(o)},_daylightSavingAdjust:function(e){return e?(e.setHours(e.getHours()>12?e.getHours()+2:0),e):null},_setDate:function(e,t,n){var r=!t,i=e.selectedMonth,s=e.selectedYear,o=this._restrictMinMax(e,this._determineDate(e,t,new Date));e.selectedDay=e.currentDay=o.getDate(),e.drawMonth=e.selectedMonth=e.currentMonth=o.getMonth(),e.drawYear=e.selectedYear=e.currentYear=o.getFullYear(),(i!==e.selectedMonth||s!==e.selectedYear)&&!n&&this._notifyChange(e),this._adjustInstDate(e),e.input&&e.input.val(r?"":this._formatDate(e))},_getDate:function(e){var t=!e.currentYear||e.input&&e.input.val()===""?null:this._daylightSavingAdjust(new Date(e.currentYear,e.currentMonth,e.currentDay));return t},_attachHandlers:function(t){var n=this._get(t,"stepMonths"),i="#"+t.id.replace(/\\\\/g,"\\");t.dpDiv.find("[data-handler]").map(function(){var t={prev:function(){window["DP_jQuery_"+r].datepicker._adjustDate(i,-n,"M")},next:function(){window["DP_jQuery_"+r].datepicker._adjustDate(i,+n,"M")},hide:function(){window["DP_jQuery_"+r].datepicker._hideDatepicker()},today:function(){window["DP_jQuery_"+r].datepicker._gotoToday(i)},selectDay:function(){return window["DP_jQuery_"+r].datepicker._selectDay(i,+this.getAttribute("data-month"),+this.getAttribute("data-year"),this),!1},selectMonth:function(){return window["DP_jQuery_"+r].datepicker._selectMonthYear(i,this,"M"),!1},selectYear:function(){return window["DP_jQuery_"+r].datepicker._selectMonthYear(i,this,"Y"),!1}};e(this).bind(this.getAttribute("data-event"),t[this.getAttribute("data-handler")])})},_generateHTML:function(e){var t,n,r,i,s,o,u,a,f,l,c,h,p,d,v,m,g,y,b,w,E,S,x,T,N,C,k,L,A,O,M,_,D,P,H,B,j,F,I,q=new Date,R=this._daylightSavingAdjust(new Date(q.getFullYear(),q.getMonth(),q.getDate())),U=this._get(e,"isRTL"),z=this._get(e,"showButtonPanel"),W=this._get(e,"hideIfNoPrevNext"),X=this._get(e,"navigationAsDateFormat"),V=this._getNumberOfMonths(e),$=this._get(e,"showCurrentAtPos"),J=this._get(e,"stepMonths"),K=V[0]!==1||V[1]!==1,Q=this._daylightSavingAdjust(e.currentDay?new Date(e.currentYear,e.currentMonth,e.currentDay):new Date(9999,9,9)),G=this._getMinMaxDate(e,"min"),Y=this._getMinMaxDate(e,"max"),Z=e.drawMonth-$,et=e.drawYear;Z<0&&(Z+=12,et--);if(Y){t=this._daylightSavingAdjust(new Date(Y.getFullYear(),Y.getMonth()-V[0]*V[1]+1,Y.getDate())),t=G&&t<G?G:t;while(this._daylightSavingAdjust(new Date(et,Z,1))>t)Z--,Z<0&&(Z=11,et--)}e.drawMonth=Z,e.drawYear=et,n=this._get(e,"prevText"),n=X?this.formatDate(n,this._daylightSavingAdjust(new Date(et,Z-J,1)),this._getFormatConfig(e)):n,r=this._canAdjustMonth(e,-1,et,Z)?"<a class='ui-datepicker-prev ui-corner-all' data-handler='prev' data-event='click' title='"+n+"'><span class='ui-icon ui-icon-circle-triangle-"+(U?"e":"w")+"'>"+n+"</span></a>":W?"":"<a class='ui-datepicker-prev ui-corner-all ui-state-disabled' title='"+n+"'><span class='ui-icon ui-icon-circle-triangle-"+(U?"e":"w")+"'>"+n+"</span></a>",i=this._get(e,"nextText"),i=X?this.formatDate(i,this._daylightSavingAdjust(new Date(et,Z+J,1)),this._getFormatConfig(e)):i,s=this._canAdjustMonth(e,1,et,Z)?"<a class='ui-datepicker-next ui-corner-all' data-handler='next' data-event='click' title='"+i+"'><span class='ui-icon ui-icon-circle-triangle-"+(U?"w":"e")+"'>"+i+"</span></a>":W?"":"<a class='ui-datepicker-next ui-corner-all ui-state-disabled' title='"+i+"'><span class='ui-icon ui-icon-circle-triangle-"+(U?"w":"e")+"'>"+i+"</span></a>",o=this._get(e,"currentText"),u=this._get(e,"gotoCurrent")&&e.currentDay?Q:R,o=X?this.formatDate(o,u,this._getFormatConfig(e)):o,a=e.inline?"":"<button type='button' class='ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all' data-handler='hide' data-event='click'>"+this._get(e,"closeText")+"</button>",f=z?"<div class='ui-datepicker-buttonpane ui-widget-content'>"+(U?a:"")+(this._isInRange(e,u)?"<button type='button' class='ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all' data-handler='today' data-event='click'>"+o+"</button>":"")+(U?"":a)+"</div>":"",l=parseInt(this._get(e,"firstDay"),10),l=isNaN(l)?0:l,c=this._get(e,"showWeek"),h=this._get(e,"dayNames"),p=this._get(e,"dayNamesMin"),d=this._get(e,"monthNames"),v=this._get(e,"monthNamesShort"),m=this._get(e,"beforeShowDay"),g=this._get(e,"showOtherMonths"),y=this._get(e,"selectOtherMonths"),b=this._getDefaultDate(e),w="",E;for(S=0;S<V[0];S++){x="",this.maxRows=4;for(T=0;T<V[1];T++){N=this._daylightSavingAdjust(new Date(et,Z,e.selectedDay)),C=" ui-corner-all",k="";if(K){k+="<div class='ui-datepicker-group";if(V[1]>1)switch(T){case 0:k+=" ui-datepicker-group-first",C=" ui-corner-"+(U?"right":"left");break;case V[1]-1:k+=" ui-datepicker-group-last",C=" ui-corner-"+(U?"left":"right");break;default:k+=" ui-datepicker-group-middle",C=""}k+="'>"}k+="<div class='ui-datepicker-header ui-widget-header ui-helper-clearfix"+C+"'>"+(/all|left/.test(C)&&S===0?U?s:r:"")+(/all|right/.test(C)&&S===0?U?r:s:"")+this._generateMonthYearHeader(e,Z,et,G,Y,S>0||T>0,d,v)+"</div><table class='ui-datepicker-calendar'><thead>"+"<tr>",L=c?"<th class='ui-datepicker-week-col'>"+this._get(e,"weekHeader")+"</th>":"";for(E=0;E<7;E++)A=(E+l)%7,L+="<th"+((E+l+6)%7>=5?" class='ui-datepicker-week-end'":"")+">"+"<span title='"+h[A]+"'>"+p[A]+"</span></th>";k+=L+"</tr></thead><tbody>",O=this._getDaysInMonth(et,Z),et===e.selectedYear&&Z===e.selectedMonth&&(e.selectedDay=Math.min(e.selectedDay,O)),M=(this._getFirstDayOfMonth(et,Z)-l+7)%7,_=Math.ceil((M+O)/7),D=K?this.maxRows>_?this.maxRows:_:_,this.maxRows=D,P=this._daylightSavingAdjust(new Date(et,Z,1-M));for(H=0;H<D;H++){k+="<tr>",B=c?"<td class='ui-datepicker-week-col'>"+this._get(e,"calculateWeek")(P)+"</td>":"";for(E=0;E<7;E++)j=m?m.apply(e.input?e.input[0]:null,[P]):[!0,""],F=P.getMonth()!==Z,I=F&&!y||!j[0]||G&&P<G||Y&&P>Y,B+="<td class='"+((E+l+6)%7>=5?" ui-datepicker-week-end":"")+(F?" ui-datepicker-other-month":"")+(P.getTime()===N.getTime()&&Z===e.selectedMonth&&e._keyEvent||b.getTime()===P.getTime()&&b.getTime()===N.getTime()?" "+this._dayOverClass:"")+(I?" "+this._unselectableClass+" ui-state-disabled":"")+(F&&!g?"":" "+j[1]+(P.getTime()===Q.getTime()?" "+this._currentClass:"")+(P.getTime()===R.getTime()?" ui-datepicker-today":""))+"'"+((!F||g)&&j[2]?" title='"+j[2].replace(/'/g,"&#39;")+"'":"")+(I?"":" data-handler='selectDay' data-event='click' data-month='"+P.getMonth()+"' data-year='"+P.getFullYear()+"'")+">"+(F&&!g?"&#xa0;":I?"<span class='ui-state-default'>"+P.getDate()+"</span>":"<a class='ui-state-default"+(P.getTime()===R.getTime()?" ui-state-highlight":"")+(P.getTime()===Q.getTime()?" ui-state-active":"")+(F?" ui-priority-secondary":"")+"' href='#'>"+P.getDate()+"</a>")+"</td>",P.setDate(P.getDate()+1),P=this._daylightSavingAdjust(P);k+=B+"</tr>"}Z++,Z>11&&(Z=0,et++),k+="</tbody></table>"+(K?"</div>"+(V[0]>0&&T===V[1]-1?"<div class='ui-datepicker-row-break'></div>":""):""),x+=k}w+=x}return w+=f,e._keyEvent=!1,w},_generateMonthYearHeader:function(e,t,n,r,i,s,o,u){var a,f,l,c,h,p,d,v,m=this._get(e,"changeMonth"),g=this._get(e,"changeYear"),y=this._get(e,"showMonthAfterYear"),b="<div class='ui-datepicker-title'>",w="";if(s||!m)w+="<span class='ui-datepicker-month'>"+o[t]+"</span>";else{a=r&&r.getFullYear()===n,f=i&&i.getFullYear()===n,w+="<select class='ui-datepicker-month' data-handler='selectMonth' data-event='change'>";for(l=0;l<12;l++)(!a||l>=r.getMonth())&&(!f||l<=i.getMonth())&&(w+="<option value='"+l+"'"+(l===t?" selected='selected'":"")+">"+u[l]+"</option>");w+="</select>"}y||(b+=w+(s||!m||!g?"&#xa0;":""));if(!e.yearshtml){e.yearshtml="";if(s||!g)b+="<span class='ui-datepicker-year'>"+n+"</span>";else{c=this._get(e,"yearRange").split(":"),h=(new Date).getFullYear(),p=function(e){var t=e.match(/c[+\-].*/)?n+parseInt(e.substring(1),10):e.match(/[+\-].*/)?h+parseInt(e,10):parseInt(e,10);return isNaN(t)?h:t},d=p(c[0]),v=Math.max(d,p(c[1]||"")),d=r?Math.max(d,r.getFullYear()):d,v=i?Math.min(v,i.getFullYear()):v,e.yearshtml+="<select class='ui-datepicker-year' data-handler='selectYear' data-event='change'>";for(;d<=v;d++)e.yearshtml+="<option value='"+d+"'"+(d===n?" selected='selected'":"")+">"+d+"</option>";e.yearshtml+="</select>",b+=e.yearshtml,e.yearshtml=null}}return b+=this._get(e,"yearSuffix"),y&&(b+=(s||!m||!g?"&#xa0;":"")+w),b+="</div>",b},_adjustInstDate:function(e,t,n){var r=e.drawYear+(n==="Y"?t:0),i=e.drawMonth+(n==="M"?t:0),s=Math.min(e.selectedDay,this._getDaysInMonth(r,i))+(n==="D"?t:0),o=this._restrictMinMax(e,this._daylightSavingAdjust(new Date(r,i,s)));e.selectedDay=o.getDate(),e.drawMonth=e.selectedMonth=o.getMonth(),e.drawYear=e.selectedYear=o.getFullYear(),(n==="M"||n==="Y")&&this._notifyChange(e)},_restrictMinMax:function(e,t){var n=this._getMinMaxDate(e,"min"),r=this._getMinMaxDate(e,"max"),i=n&&t<n?n:t;return r&&i>r?r:i},_notifyChange:function(e){var t=this._get(e,"onChangeMonthYear");t&&t.apply(e.input?e.input[0]:null,[e.selectedYear,e.selectedMonth+1,e])},_getNumberOfMonths:function(e){var t=this._get(e,"numberOfMonths");return t==null?[1,1]:typeof t=="number"?[1,t]:t},_getMinMaxDate:function(e,t){return this._determineDate(e,this._get(e,t+"Date"),null)},_getDaysInMonth:function(e,t){return 32-this._daylightSavingAdjust(new Date(e,t,32)).getDate()},_getFirstDayOfMonth:function(e,t){return(new Date(e,t,1)).getDay()},_canAdjustMonth:function(e,t,n,r){var i=this._getNumberOfMonths(e),s=this._daylightSavingAdjust(new Date(n,r+(t<0?t:i[0]*i[1]),1));return t<0&&s.setDate(this._getDaysInMonth(s.getFullYear(),s.getMonth())),this._isInRange(e,s)},_isInRange:function(e,t){var n,r,i=this._getMinMaxDate(e,"min"),s=this._getMinMaxDate(e,"max"),o=null,u=null,a=this._get(e,"yearRange");return a&&(n=a.split(":"),r=(new Date).getFullYear(),o=parseInt(n[0],10),u=parseInt(n[1],10),n[0].match(/[+\-].*/)&&(o+=r),n[1].match(/[+\-].*/)&&(u+=r)),(!i||t.getTime()>=i.getTime())&&(!s||t.getTime()<=s.getTime())&&(!o||t.getFullYear()>=o)&&(!u||t.getFullYear()<=u)},_getFormatConfig:function(e){var t=this._get(e,"shortYearCutoff");return t=typeof t!="string"?t:(new Date).getFullYear()%100+parseInt(t,10),{shortYearCutoff:t,dayNamesShort:this._get(e,"dayNamesShort"),dayNames:this._get(e,"dayNames"),monthNamesShort:this._get(e,"monthNamesShort"),monthNames:this._get(e,"monthNames")}},_formatDate:function(e,t,n,r){t||(e.currentDay=e.selectedDay,e.currentMonth=e.selectedMonth,e.currentYear=e.selectedYear);var i=t?typeof t=="object"?t:this._daylightSavingAdjust(new Date(r,n,t)):this._daylightSavingAdjust(new Date(e.currentYear,e.currentMonth,e.currentDay));return this.formatDate(this._get(e,"dateFormat"),i,this._getFormatConfig(e))}}),e.fn.datepicker=function(t){if(!this.length)return this;e.datepicker.initialized||(e(document).mousedown(e.datepicker._checkExternalClick),e.datepicker.initialized=!0),e("#"+e.datepicker._mainDivId).length===0&&e("body").append(e.datepicker.dpDiv);var n=Array.prototype.slice.call(arguments,1);return typeof t!="string"||t!=="isDisabled"&&t!=="getDate"&&t!=="widget"?t==="option"&&arguments.length===2&&typeof arguments[1]=="string"?e.datepicker["_"+t+"Datepicker"].apply(e.datepicker,[this[0]].concat(n)):this.each(function(){typeof t=="string"?e.datepicker["_"+t+"Datepicker"].apply(e.datepicker,[this].concat(n)):e.datepicker._attachDatepicker(this,t)}):e.datepicker["_"+t+"Datepicker"].apply(e.datepicker,[this[0]].concat(n))},e.datepicker=new s,e.datepicker.initialized=!1,e.datepicker.uuid=(new Date).getTime(),e.datepicker.version="1.10.1",window["DP_jQuery_"+r]=e})(jQuery);(function(e,t){var n={buttons:!0,height:!0,maxHeight:!0,maxWidth:!0,minHeight:!0,minWidth:!0,width:!0},r={maxHeight:!0,maxWidth:!0,minHeight:!0,minWidth:!0};e.widget("ui.dialog",{version:"1.10.1",options:{appendTo:"body",autoOpen:!0,buttons:[],closeOnEscape:!0,closeText:"close",dialogClass:"",draggable:!0,hide:null,height:"auto",maxHeight:null,maxWidth:null,minHeight:150,minWidth:150,modal:!1,position:{my:"center",at:"center",of:window,collision:"fit",using:function(t){var n=e(this).css(t).offset().top;n<0&&e(this).css("top",t.top-n)}},resizable:!0,show:null,title:null,width:300,beforeClose:null,close:null,drag:null,dragStart:null,dragStop:null,focus:null,open:null,resize:null,resizeStart:null,resizeStop:null},_create:function(){this.originalCss={display:this.element[0].style.display,width:this.element[0].style.width,minHeight:this.element[0].style.minHeight,maxHeight:this.element[0].style.maxHeight,height:this.element[0].style.height},this.originalPosition={parent:this.element.parent(),index:this.element.parent().children().index(this.element)},this.originalTitle=this.element.attr("title"),this.options.title=this.options.title||this.originalTitle,this._createWrapper(),this.element.show().removeAttr("title").addClass("ui-dialog-content ui-widget-content").appendTo(this.uiDialog),this._createTitlebar(),this._createButtonPane(),this.options.draggable&&e.fn.draggable&&this._makeDraggable(),this.options.resizable&&e.fn.resizable&&this._makeResizable(),this._isOpen=!1},_init:function(){this.options.autoOpen&&this.open()},_appendTo:function(){var t=this.options.appendTo;return t&&(t.jquery||t.nodeType)?e(t):this.document.find(t||"body").eq(0)},_destroy:function(){var e,t=this.originalPosition;this._destroyOverlay(),this.element.removeUniqueId().removeClass("ui-dialog-content ui-widget-content").css(this.originalCss).detach(),this.uiDialog.stop(!0,!0).remove(),this.originalTitle&&this.element.attr("title",this.originalTitle),e=t.parent.children().eq(t.index),e.length&&e[0]!==this.element[0]?e.before(this.element):t.parent.append(this.element)},widget:function(){return this.uiDialog},disable:e.noop,enable:e.noop,close:function(t){var n=this;if(!this._isOpen||this._trigger("beforeClose",t)===!1)return;this._isOpen=!1,this._destroyOverlay(),this.opener.filter(":focusable").focus().length||e(this.document[0].activeElement).blur(),this._hide(this.uiDialog,this.options.hide,function(){n._trigger("close",t)})},isOpen:function(){return this._isOpen},moveToTop:function(){this._moveToTop()},_moveToTop:function(e,t){var n=!!this.uiDialog.nextAll(":visible").insertBefore(this.uiDialog).length;return n&&!t&&this._trigger("focus",e),n},open:function(){var t=this;if(this._isOpen){this._moveToTop()&&this._focusTabbable();return}this._isOpen=!0,this.opener=e(this.document[0].activeElement),this._size(),this._position(),this._createOverlay(),this._moveToTop(null,!0),this._show(this.uiDialog,this.options.show,function(){t._focusTabbable(),t._trigger("focus")}),this._trigger("open")},_focusTabbable:function(){var e=this.element.find("[autofocus]");e.length||(e=this.element.find(":tabbable")),e.length||(e=this.uiDialogButtonPane.find(":tabbable")),e.length||(e=this.uiDialogTitlebarClose.filter(":tabbable")),e.length||(e=this.uiDialog),e.eq(0).focus()},_keepFocus:function(t){function n(){var t=this.document[0].activeElement,n=this.uiDialog[0]===t||e.contains(this.uiDialog[0],t);n||this._focusTabbable()}t.preventDefault(),n.call(this),this._delay(n)},_createWrapper:function(){this.uiDialog=e("<div>").addClass("ui-dialog ui-widget ui-widget-content ui-corner-all ui-front "+this.options.dialogClass).hide().attr({tabIndex:-1,role:"dialog"}).appendTo(this._appendTo()),this._on(this.uiDialog,{keydown:function(t){if(this.options.closeOnEscape&&!t.isDefaultPrevented()&&t.keyCode&&t.keyCode===e.ui.keyCode.ESCAPE){t.preventDefault(),this.close(t);return}if(t.keyCode!==e.ui.keyCode.TAB)return;var n=this.uiDialog.find(":tabbable"),r=n.filter(":first"),i=n.filter(":last");t.target!==i[0]&&t.target!==this.uiDialog[0]||!!t.shiftKey?(t.target===r[0]||t.target===this.uiDialog[0])&&t.shiftKey&&(i.focus(1),t.preventDefault()):(r.focus(1),t.preventDefault())},mousedown:function(e){this._moveToTop(e)&&this._focusTabbable()}}),this.element.find("[aria-describedby]").length||this.uiDialog.attr({"aria-describedby":this.element.uniqueId().attr("id")})},_createTitlebar:function(){var t;this.uiDialogTitlebar=e("<div>").addClass("ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix").prependTo(this.uiDialog),this._on(this.uiDialogTitlebar,{mousedown:function(t){e(t.target).closest(".ui-dialog-titlebar-close")||this.uiDialog.focus()}}),this.uiDialogTitlebarClose=e("<button></button>").button({label:this.options.closeText,icons:{primary:"ui-icon-closethick"},text:!1}).addClass("ui-dialog-titlebar-close").appendTo(this.uiDialogTitlebar),this._on(this.uiDialogTitlebarClose,{click:function(e){e.preventDefault(),this.close(e)}}),t=e("<span>").uniqueId().addClass("ui-dialog-title").prependTo(this.uiDialogTitlebar),this._title(t),this.uiDialog.attr({"aria-labelledby":t.attr("id")})},_title:function(e){this.options.title||e.html("&#160;"),e.text(this.options.title)},_createButtonPane:function(){this.uiDialogButtonPane=e("<div>").addClass("ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"),this.uiButtonSet=e("<div>").addClass("ui-dialog-buttonset").appendTo(this.uiDialogButtonPane),this._createButtons()},_createButtons:function(){var t=this,n=this.options.buttons;this.uiDialogButtonPane.remove(),this.uiButtonSet.empty();if(e.isEmptyObject(n)||e.isArray(n)&&!n.length){this.uiDialog.removeClass("ui-dialog-buttons");return}e.each(n,function(n,r){var i,s;r=e.isFunction(r)?{click:r,text:n}:r,r=e.extend({type:"button"},r),i=r.click,r.click=function(){i.apply(t.element[0],arguments)},s={icons:r.icons,text:r.showText},delete r.icons,delete r.showText,e("<button></button>",r).button(s).appendTo(t.uiButtonSet)}),this.uiDialog.addClass("ui-dialog-buttons"),this.uiDialogButtonPane.appendTo(this.uiDialog)},_makeDraggable:function(){function r(e){return{position:e.position,offset:e.offset}}var t=this,n=this.options;this.uiDialog.draggable({cancel:".ui-dialog-content, .ui-dialog-titlebar-close",handle:".ui-dialog-titlebar",containment:"document",start:function(n,i){e(this).addClass("ui-dialog-dragging"),t._blockFrames(),t._trigger("dragStart",n,r(i))},drag:function(e,n){t._trigger("drag",e,r(n))},stop:function(i,s){n.position=[s.position.left-t.document.scrollLeft(),s.position.top-t.document.scrollTop()],e(this).removeClass("ui-dialog-dragging"),t._unblockFrames(),t._trigger("dragStop",i,r(s))}})},_makeResizable:function(){function o(e){return{originalPosition:e.originalPosition,originalSize:e.originalSize,position:e.position,size:e.size}}var t=this,n=this.options,r=n.resizable,i=this.uiDialog.css("position"),s=typeof r=="string"?r:"n,e,s,w,se,sw,ne,nw";this.uiDialog.resizable({cancel:".ui-dialog-content",containment:"document",alsoResize:this.element,maxWidth:n.maxWidth,maxHeight:n.maxHeight,minWidth:n.minWidth,minHeight:this._minHeight(),handles:s,start:function(n,r){e(this).addClass("ui-dialog-resizing"),t._blockFrames(),t._trigger("resizeStart",n,o(r))},resize:function(e,n){t._trigger("resize",e,o(n))},stop:function(r,i){n.height=e(this).height(),n.width=e(this).width(),e(this).removeClass("ui-dialog-resizing"),t._unblockFrames(),t._trigger("resizeStop",r,o(i))}}).css("position",i)},_minHeight:function(){var e=this.options;return e.height==="auto"?e.minHeight:Math.min(e.minHeight,e.height)},_position:function(){var e=this.uiDialog.is(":visible");e||this.uiDialog.show(),this.uiDialog.position(this.options.position),e||this.uiDialog.hide()},_setOptions:function(t){var i=this,s=!1,o={};e.each(t,function(e,t){i._setOption(e,t),e in n&&(s=!0),e in r&&(o[e]=t)}),s&&(this._size(),this._position()),this.uiDialog.is(":data(ui-resizable)")&&this.uiDialog.resizable("option",o)},_setOption:function(e,t){var n,r,i=this.uiDialog;e==="dialogClass"&&i.removeClass(this.options.dialogClass).addClass(t);if(e==="disabled")return;this._super(e,t),e==="appendTo"&&this.uiDialog.appendTo(this._appendTo()),e==="buttons"&&this._createButtons(),e==="closeText"&&this.uiDialogTitlebarClose.button({label:""+t}),e==="draggable"&&(n=i.is(":data(ui-draggable)"),n&&!t&&i.draggable("destroy"),!n&&t&&this._makeDraggable()),e==="position"&&this._position(),e==="resizable"&&(r=i.is(":data(ui-resizable)"),r&&!t&&i.resizable("destroy"),r&&typeof t=="string"&&i.resizable("option","handles",t),!r&&t!==!1&&this._makeResizable()),e==="title"&&this._title(this.uiDialogTitlebar.find(".ui-dialog-title"))},_size:function(){var e,t,n,r=this.options;this.element.show().css({width:"auto",minHeight:0,maxHeight:"none",height:0}),r.minWidth>r.width&&(r.width=r.minWidth),e=this.uiDialog.css({height:"auto",width:r.width}).outerHeight(),t=Math.max(0,r.minHeight-e),n=typeof r.maxHeight=="number"?Math.max(0,r.maxHeight-e):"none",r.height==="auto"?this.element.css({minHeight:t,maxHeight:n,height:"auto"}):this.element.height(Math.max(0,r.height-e)),this.uiDialog.is(":data(ui-resizable)")&&this.uiDialog.resizable("option","minHeight",this._minHeight())},_blockFrames:function(){this.iframeBlocks=this.document.find("iframe").map(function(){var t=e(this);return e("<div>").css({position:"absolute",width:t.outerWidth(),height:t.outerHeight()}).appendTo(t.parent()).offset(t.offset())[0]})},_unblockFrames:function(){this.iframeBlocks&&(this.iframeBlocks.remove(),delete this.iframeBlocks)},_createOverlay:function(){if(!this.options.modal)return;e.ui.dialog.overlayInstances||this._delay(function(){e.ui.dialog.overlayInstances&&this.document.bind("focusin.dialog",function(t){!e(t.target).closest(".ui-dialog").length&&!e(t.target).closest(".ui-datepicker").length&&(t.preventDefault(),e(".ui-dialog:visible:last .ui-dialog-content").data("ui-dialog")._focusTabbable())})}),this.overlay=e("<div>").addClass("ui-widget-overlay ui-front").appendTo(this._appendTo()),this._on(this.overlay,{mousedown:"_keepFocus"}),e.ui.dialog.overlayInstances++},_destroyOverlay:function(){if(!this.options.modal)return;this.overlay&&(e.ui.dialog.overlayInstances--,e.ui.dialog.overlayInstances||this.document.unbind("focusin.dialog"),this.overlay.remove(),this.overlay=null)}}),e.ui.dialog.overlayInstances=0,e.uiBackCompat!==!1&&e.widget("ui.dialog",e.ui.dialog,{_position:function(){var t=this.options.position,n=[],r=[0,0],i;if(t){if(typeof t=="string"||typeof t=="object"&&"0"in t)n=t.split?t.split(" "):[t[0],t[1]],n.length===1&&(n[1]=n[0]),e.each(["left","top"],function(e,t){+n[e]===n[e]&&(r[e]=n[e],n[e]=t)}),t={my:n[0]+(r[0]<0?r[0]:"+"+r[0])+" "+n[1]+(r[1]<0?r[1]:"+"+r[1]),at:n.join(" ")};t=e.extend({},e.ui.dialog.prototype.options.position,t)}else t=e.ui.dialog.prototype.options.position;i=this.uiDialog.is(":visible"),i||this.uiDialog.show(),this.uiDialog.position(t),i||this.uiDialog.hide()}})})(jQuery);(function(e,t){e.widget("ui.progressbar",{version:"1.10.1",options:{max:100,value:0,change:null,complete:null},min:0,_create:function(){this.oldValue=this.options.value=this._constrainedValue(),this.element.addClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").attr({role:"progressbar","aria-valuemin":this.min}),this.valueDiv=e("<div class='ui-progressbar-value ui-widget-header ui-corner-left'></div>").appendTo(this.element),this._refreshValue()},_destroy:function(){this.element.removeClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").removeAttr("role").removeAttr("aria-valuemin").removeAttr("aria-valuemax").removeAttr("aria-valuenow"),this.valueDiv.remove()},value:function(e){if(e===t)return this.options.value;this.options.value=this._constrainedValue(e),this._refreshValue()},_constrainedValue:function(e){return e===t&&(e=this.options.value),this.indeterminate=e===!1,typeof e!="number"&&(e=0),this.indeterminate?!1:Math.min(this.options.max,Math.max(this.min,e))},_setOptions:function(e){var t=e.value;delete e.value,this._super(e),this.options.value=this._constrainedValue(t),this._refreshValue()},_setOption:function(e,t){e==="max"&&(t=Math.max(this.min,t)),this._super(e,t)},_percentage:function(){return this.indeterminate?100:100*(this.options.value-this.min)/(this.options.max-this.min)},_refreshValue:function(){var t=this.options.value,n=this._percentage();this.valueDiv.toggle(this.indeterminate||t>this.min).toggleClass("ui-corner-right",t===this.options.max).width(n.toFixed(0)+"%"),this.element.toggleClass("ui-progressbar-indeterminate",this.indeterminate),this.indeterminate?(this.element.removeAttr("aria-valuenow"),this.overlayDiv||(this.overlayDiv=e("<div class='ui-progressbar-overlay'></div>").appendTo(this.valueDiv))):(this.element.attr({"aria-valuemax":this.options.max,"aria-valuenow":t}),this.overlayDiv&&(this.overlayDiv.remove(),this.overlayDiv=null)),this.oldValue!==t&&(this.oldValue=t,this._trigger("change")),t===this.options.max&&this._trigger("complete")}})})(jQuery);(function(e){function t(e){return function(){var t=this.element.val();e.apply(this,arguments),this._refresh(),t!==this.element.val()&&this._trigger("change")}}e.widget("ui.spinner",{version:"1.10.1",defaultElement:"<input>",widgetEventPrefix:"spin",options:{culture:null,icons:{down:"ui-icon-triangle-1-s",up:"ui-icon-triangle-1-n"},incremental:!0,max:null,min:null,numberFormat:null,page:10,step:1,change:null,spin:null,start:null,stop:null},_create:function(){this._setOption("max",this.options.max),this._setOption("min",this.options.min),this._setOption("step",this.options.step),this._value(this.element.val(),!0),this._draw(),this._on(this._events),this._refresh(),this._on(this.window,{beforeunload:function(){this.element.removeAttr("autocomplete")}})},_getCreateOptions:function(){var t={},n=this.element;return e.each(["min","max","step"],function(e,r){var i=n.attr(r);i!==undefined&&i.length&&(t[r]=i)}),t},_events:{keydown:function(e){this._start(e)&&this._keydown(e)&&e.preventDefault()},keyup:"_stop",focus:function(){this.previous=this.element.val()},blur:function(e){if(this.cancelBlur){delete this.cancelBlur;return}this._refresh(),this.previous!==this.element.val()&&this._trigger("change",e)},mousewheel:function(e,t){if(!t)return;if(!this.spinning&&!this._start(e))return!1;this._spin((t>0?1:-1)*this.options.step,e),clearTimeout(this.mousewheelTimer),this.mousewheelTimer=this._delay(function(){this.spinning&&this._stop(e)},100),e.preventDefault()},"mousedown .ui-spinner-button":function(t){function r(){var e=this.element[0]===this.document[0].activeElement;e||(this.element.focus(),this.previous=n,this._delay(function(){this.previous=n}))}var n;n=this.element[0]===this.document[0].activeElement?this.previous:this.element.val(),t.preventDefault(),r.call(this),this.cancelBlur=!0,this._delay(function(){delete this.cancelBlur,r.call(this)});if(this._start(t)===!1)return;this._repeat(null,e(t.currentTarget).hasClass("ui-spinner-up")?1:-1,t)},"mouseup .ui-spinner-button":"_stop","mouseenter .ui-spinner-button":function(t){if(!e(t.currentTarget).hasClass("ui-state-active"))return;if(this._start(t)===!1)return!1;this._repeat(null,e(t.currentTarget).hasClass("ui-spinner-up")?1:-1,t)},"mouseleave .ui-spinner-button":"_stop"},_draw:function(){var e=this.uiSpinner=this.element.addClass("ui-spinner-input").attr("autocomplete","off").wrap(this._uiSpinnerHtml()).parent().append(this._buttonHtml());this.element.attr("role","spinbutton"),this.buttons=e.find(".ui-spinner-button").attr("tabIndex",-1).button().removeClass("ui-corner-all"),this.buttons.height()>Math.ceil(e.height()*.5)&&e.height()>0&&e.height(e.height()),this.options.disabled&&this.disable()},_keydown:function(t){var n=this.options,r=e.ui.keyCode;switch(t.keyCode){case r.UP:return this._repeat(null,1,t),!0;case r.DOWN:return this._repeat(null,-1,t),!0;case r.PAGE_UP:return this._repeat(null,n.page,t),!0;case r.PAGE_DOWN:return this._repeat(null,-n.page,t),!0}return!1},_uiSpinnerHtml:function(){return"<span class='ui-spinner ui-widget ui-widget-content ui-corner-all'></span>"},_buttonHtml:function(){return"<a class='ui-spinner-button ui-spinner-up ui-corner-tr'><span class='ui-icon "+this.options.icons.up+"'>&#9650;</span>"+"</a>"+"<a class='ui-spinner-button ui-spinner-down ui-corner-br'>"+"<span class='ui-icon "+this.options.icons.down+"'>&#9660;</span>"+"</a>"},_start:function(e){return!this.spinning&&this._trigger("start",e)===!1?!1:(this.counter||(this.counter=1),this.spinning=!0,!0)},_repeat:function(e,t,n){e=e||500,clearTimeout(this.timer),this.timer=this._delay(function(){this._repeat(40,t,n)},e),this._spin(t*this.options.step,n)},_spin:function(e,t){var n=this.value()||0;this.counter||(this.counter=1),n=this._adjustValue(n+e*this._increment(this.counter));if(!this.spinning||this._trigger("spin",t,{value:n})!==!1)this._value(n),this.counter++},_increment:function(t){var n=this.options.incremental;return n?e.isFunction(n)?n(t):Math.floor(t*t*t/5e4-t*t/500+17*t/200+1):1},_precision:function(){var e=this._precisionOf(this.options.step);return this.options.min!==null&&(e=Math.max(e,this._precisionOf(this.options.min))),e},_precisionOf:function(e){var t=e.toString(),n=t.indexOf(".");return n===-1?0:t.length-n-1},_adjustValue:function(e){var t,n,r=this.options;return t=r.min!==null?r.min:0,n=e-t,n=Math.round(n/r.step)*r.step,e=t+n,e=parseFloat(e.toFixed(this._precision())),r.max!==null&&e>r.max?r.max:r.min!==null&&e<r.min?r.min:e},_stop:function(e){if(!this.spinning)return;clearTimeout(this.timer),clearTimeout(this.mousewheelTimer),this.counter=0,this.spinning=!1,this._trigger("stop",e)},_setOption:function(e,t){if(e==="culture"||e==="numberFormat"){var n=this._parse(this.element.val());this.options[e]=t,this.element.val(this._format(n));return}(e==="max"||e==="min"||e==="step")&&typeof t=="string"&&(t=this._parse(t)),e==="icons"&&(this.buttons.first().find(".ui-icon").removeClass(this.options.icons.up).addClass(t.up),this.buttons.last().find(".ui-icon").removeClass(this.options.icons.down).addClass(t.down)),this._super(e,t),e==="disabled"&&(t?(this.element.prop("disabled",!0),this.buttons.button("disable")):(this.element.prop("disabled",!1),this.buttons.button("enable")))},_setOptions:t(function(e){this._super(e),this._value(this.element.val())}),_parse:function(e){return typeof e=="string"&&e!==""&&(e=window.Globalize&&this.options.numberFormat?Globalize.parseFloat(e,10,this.options.culture):+e),e===""||isNaN(e)?null:e},_format:function(e){return e===""?"":window.Globalize&&this.options.numberFormat?Globalize.format(e,this.options.numberFormat,this.options.culture):e},_refresh:function(){this.element.attr({"aria-valuemin":this.options.min,"aria-valuemax":this.options.max,"aria-valuenow":this._parse(this.element.val())})},_value:function(e,t){var n;e!==""&&(n=this._parse(e),n!==null&&(t||(n=this._adjustValue(n)),e=this._format(n))),this.element.val(e),this._refresh()},_destroy:function(){this.element.removeClass("ui-spinner-input").prop("disabled",!1).removeAttr("autocomplete").removeAttr("role").removeAttr("aria-valuemin").removeAttr("aria-valuemax").removeAttr("aria-valuenow"),this.uiSpinner.replaceWith(this.element)},stepUp:t(function(e){this._stepUp(e)}),_stepUp:function(e){this._start()&&(this._spin((e||1)*this.options.step),this._stop())},stepDown:t(function(e){this._stepDown(e)}),_stepDown:function(e){this._start()&&(this._spin((e||1)*-this.options.step),this._stop())},pageUp:t(function(e){this._stepUp((e||1)*this.options.page)}),pageDown:t(function(e){this._stepDown((e||1)*this.options.page)}),value:function(e){if(!arguments.length)return this._parse(this.element.val());t(this._value).call(this,e)},widget:function(){return this.uiSpinner}})})(jQuery);(function(e){function n(t,n){var r=(t.attr("aria-describedby")||"").split(/\s+/);r.push(n),t.data("ui-tooltip-id",n).attr("aria-describedby",e.trim(r.join(" ")))}function r(t){var n=t.data("ui-tooltip-id"),r=(t.attr("aria-describedby")||"").split(/\s+/),i=e.inArray(n,r);i!==-1&&r.splice(i,1),t.removeData("ui-tooltip-id"),r=e.trim(r.join(" ")),r?t.attr("aria-describedby",r):t.removeAttr("aria-describedby")}var t=0;e.widget("ui.tooltip",{version:"1.10.1",options:{content:function(){var t=e(this).attr("title")||"";return e("<a>").text(t).html()},hide:!0,items:"[title]:not([disabled])",position:{my:"left top+15",at:"left bottom",collision:"flipfit flip"},show:!0,tooltipClass:null,track:!1,close:null,open:null},_create:function(){this._on({mouseover:"open",focusin:"open"}),this.tooltips={},this.parents={},this.options.disabled&&this._disable()},_setOption:function(t,n){var r=this;if(t==="disabled"){this[n?"_disable":"_enable"](),this.options[t]=n;return}this._super(t,n),t==="content"&&e.each(this.tooltips,function(e,t){r._updateContent(t)})},_disable:function(){var t=this;e.each(this.tooltips,function(n,r){var i=e.Event("blur");i.target=i.currentTarget=r[0],t.close(i,!0)}),this.element.find(this.options.items).addBack().each(function(){var t=e(this);t.is("[title]")&&t.data("ui-tooltip-title",t.attr("title")).attr("title","")})},_enable:function(){this.element.find(this.options.items).addBack().each(function(){var t=e(this);t.data("ui-tooltip-title")&&t.attr("title",t.data("ui-tooltip-title"))})},open:function(t){var n=this,r=e(t?t.target:this.element).closest(this.options.items);if(!r.length||r.data("ui-tooltip-id"))return;r.attr("title")&&r.data("ui-tooltip-title",r.attr("title")),r.data("ui-tooltip-open",!0),t&&t.type==="mouseover"&&r.parents().each(function(){var t=e(this),r;t.data("ui-tooltip-open")&&(r=e.Event("blur"),r.target=r.currentTarget=this,n.close(r,!0)),t.attr("title")&&(t.uniqueId(),n.parents[this.id]={element:this,title:t.attr("title")},t.attr("title",""))}),this._updateContent(r,t)},_updateContent:function(e,t){var n,r=this.options.content,i=this,s=t?t.type:null;if(typeof r=="string")return this._open(t,e,r);n=r.call(e[0],function(n){if(!e.data("ui-tooltip-open"))return;i._delay(function(){t&&(t.type=s),this._open(t,e,n)})}),n&&this._open(t,e,n)},_open:function(t,r,i){function f(e){a.of=e;if(s.is(":hidden"))return;s.position(a)}var s,o,u,a=e.extend({},this.options.position);if(!i)return;s=this._find(r);if(s.length){s.find(".ui-tooltip-content").html(i);return}r.is("[title]")&&(t&&t.type==="mouseover"?r.attr("title",""):r.removeAttr("title")),s=this._tooltip(r),n(r,s.attr("id")),s.find(".ui-tooltip-content").html(i),this.options.track&&t&&/^mouse/.test(t.type)?(this._on(this.document,{mousemove:f}),f(t)):s.position(e.extend({of:r},this.options.position)),s.hide(),this._show(s,this.options.show),this.options.show&&this.options.show.delay&&(u=this.delayedShow=setInterval(function(){s.is(":visible")&&(f(a.of),clearInterval(u))},e.fx.interval)),this._trigger("open",t,{tooltip:s}),o={keyup:function(t){if(t.keyCode===e.ui.keyCode.ESCAPE){var n=e.Event(t);n.currentTarget=r[0],this.close(n,!0)}},remove:function(){this._removeTooltip(s)}};if(!t||t.type==="mouseover")o.mouseleave="close";if(!t||t.type==="focusin")o.focusout="close";this._on(!0,r,o)},close:function(t){var n=this,i=e(t?t.currentTarget:this.element),s=this._find(i);if(this.closing)return;clearInterval(this.delayedShow),i.data("ui-tooltip-title")&&i.attr("title",i.data("ui-tooltip-title")),r(i),s.stop(!0),this._hide(s,this.options.hide,function(){n._removeTooltip(e(this))}),i.removeData("ui-tooltip-open"),this._off(i,"mouseleave focusout keyup"),i[0]!==this.element[0]&&this._off(i,"remove"),this._off(this.document,"mousemove"),t&&t.type==="mouseleave"&&e.each(this.parents,function(t,r){e(r.element).attr("title",r.title),delete n.parents[t]}),this.closing=!0,this._trigger("close",t,{tooltip:s}),this.closing=!1},_tooltip:function(n){var r="ui-tooltip-"+t++,i=e("<div>").attr({id:r,role:"tooltip"}).addClass("ui-tooltip ui-widget ui-corner-all ui-widget-content "+(this.options.tooltipClass||""));return e("<div>").addClass("ui-tooltip-content").appendTo(i),i.appendTo(this.document[0].body),this.tooltips[r]=n,i},_find:function(t){var n=t.data("ui-tooltip-id");return n?e("#"+n):e()},_removeTooltip:function(e){e.remove(),delete this.tooltips[e.attr("id")]},_destroy:function(){var t=this;e.each(this.tooltips,function(n,r){var i=e.Event("blur");i.target=i.currentTarget=r[0],t.close(i,!0),e("#"+n).remove(),r.data("ui-tooltip-title")&&(r.attr("title",r.data("ui-tooltip-title")),r.removeData("ui-tooltip-title"))})}})})(jQuery);jQuery.effects||function(e,t){var n="ui-effects-";e.effects={effect:{}},function(e,t){function h(e,t,n){var r=u[t.type]||{};return e==null?n||!t.def?null:t.def:(e=r.floor?~~e:parseFloat(e),isNaN(e)?t.def:r.mod?(e+r.mod)%r.mod:0>e?0:r.max<e?r.max:e)}function p(t){var n=s(),r=n._rgba=[];return t=t.toLowerCase(),c(i,function(e,i){var s,u=i.re.exec(t),a=u&&i.parse(u),f=i.space||"rgba";if(a)return s=n[f](a),n[o[f].cache]=s[o[f].cache],r=n._rgba=s._rgba,!1}),r.length?(r.join()==="0,0,0,0"&&e.extend(r,l.transparent),n):l[t]}function d(e,t,n){return n=(n+1)%1,n*6<1?e+(t-e)*n*6:n*2<1?t:n*3<2?e+(t-e)*(2/3-n)*6:e}var n="backgroundColor borderBottomColor borderLeftColor borderRightColor borderTopColor color columnRuleColor outlineColor textDecorationColor textEmphasisColor",r=/^([\-+])=\s*(\d+\.?\d*)/,i=[{re:/rgba?\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,parse:function(e){return[e[1],e[2],e[3],e[4]]}},{re:/rgba?\(\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,parse:function(e){return[e[1]*2.55,e[2]*2.55,e[3]*2.55,e[4]]}},{re:/#([a-f0-9]{2})([a-f0-9]{2})([a-f0-9]{2})/,parse:function(e){return[parseInt(e[1],16),parseInt(e[2],16),parseInt(e[3],16)]}},{re:/#([a-f0-9])([a-f0-9])([a-f0-9])/,parse:function(e){return[parseInt(e[1]+e[1],16),parseInt(e[2]+e[2],16),parseInt(e[3]+e[3],16)]}},{re:/hsla?\(\s*(\d+(?:\.\d+)?)\s*,\s*(\d+(?:\.\d+)?)\%\s*,\s*(\d+(?:\.\d+)?)\%\s*(?:,\s*(\d?(?:\.\d+)?)\s*)?\)/,space:"hsla",parse:function(e){return[e[1],e[2]/100,e[3]/100,e[4]]}}],s=e.Color=function(t,n,r,i){return new e.Color.fn.parse(t,n,r,i)},o={rgba:{props:{red:{idx:0,type:"byte"},green:{idx:1,type:"byte"},blue:{idx:2,type:"byte"}}},hsla:{props:{hue:{idx:0,type:"degrees"},saturation:{idx:1,type:"percent"},lightness:{idx:2,type:"percent"}}}},u={"byte":{floor:!0,max:255},percent:{max:1},degrees:{mod:360,floor:!0}},a=s.support={},f=e("<p>")[0],l,c=e.each;f.style.cssText="background-color:rgba(1,1,1,.5)",a.rgba=f.style.backgroundColor.indexOf("rgba")>-1,c(o,function(e,t){t.cache="_"+e,t.props.alpha={idx:3,type:"percent",def:1}}),s.fn=e.extend(s.prototype,{parse:function(n,r,i,u){if(n===t)return this._rgba=[null,null,null,null],this;if(n.jquery||n.nodeType)n=e(n).css(r),r=t;var a=this,f=e.type(n),d=this._rgba=[];r!==t&&(n=[n,r,i,u],f="array");if(f==="string")return this.parse(p(n)||l._default);if(f==="array")return c(o.rgba.props,function(e,t){d[t.idx]=h(n[t.idx],t)}),this;if(f==="object")return n instanceof s?c(o,function(e,t){n[t.cache]&&(a[t.cache]=n[t.cache].slice())}):c(o,function(t,r){var i=r.cache;c(r.props,function(e,t){if(!a[i]&&r.to){if(e==="alpha"||n[e]==null)return;a[i]=r.to(a._rgba)}a[i][t.idx]=h(n[e],t,!0)}),a[i]&&e.inArray(null,a[i].slice(0,3))<0&&(a[i][3]=1,r.from&&(a._rgba=r.from(a[i])))}),this},is:function(e){var t=s(e),n=!0,r=this;return c(o,function(e,i){var s,o=t[i.cache];return o&&(s=r[i.cache]||i.to&&i.to(r._rgba)||[],c(i.props,function(e,t){if(o[t.idx]!=null)return n=o[t.idx]===s[t.idx],n})),n}),n},_space:function(){var e=[],t=this;return c(o,function(n,r){t[r.cache]&&e.push(n)}),e.pop()},transition:function(e,t){var n=s(e),r=n._space(),i=o[r],a=this.alpha()===0?s("transparent"):this,f=a[i.cache]||i.to(a._rgba),l=f.slice();return n=n[i.cache],c(i.props,function(e,r){var i=r.idx,s=f[i],o=n[i],a=u[r.type]||{};if(o===null)return;s===null?l[i]=o:(a.mod&&(o-s>a.mod/2?s+=a.mod:s-o>a.mod/2&&(s-=a.mod)),l[i]=h((o-s)*t+s,r))}),this[r](l)},blend:function(t){if(this._rgba[3]===1)return this;var n=this._rgba.slice(),r=n.pop(),i=s(t)._rgba;return s(e.map(n,function(e,t){return(1-r)*i[t]+r*e}))},toRgbaString:function(){var t="rgba(",n=e.map(this._rgba,function(e,t){return e==null?t>2?1:0:e});return n[3]===1&&(n.pop(),t="rgb("),t+n.join()+")"},toHslaString:function(){var t="hsla(",n=e.map(this.hsla(),function(e,t){return e==null&&(e=t>2?1:0),t&&t<3&&(e=Math.round(e*100)+"%"),e});return n[3]===1&&(n.pop(),t="hsl("),t+n.join()+")"},toHexString:function(t){var n=this._rgba.slice(),r=n.pop();return t&&n.push(~~(r*255)),"#"+e.map(n,function(e){return e=(e||0).toString(16),e.length===1?"0"+e:e}).join("")},toString:function(){return this._rgba[3]===0?"transparent":this.toRgbaString()}}),s.fn.parse.prototype=s.fn,o.hsla.to=function(e){if(e[0]==null||e[1]==null||e[2]==null)return[null,null,null,e[3]];var t=e[0]/255,n=e[1]/255,r=e[2]/255,i=e[3],s=Math.max(t,n,r),o=Math.min(t,n,r),u=s-o,a=s+o,f=a*.5,l,c;return o===s?l=0:t===s?l=60*(n-r)/u+360:n===s?l=60*(r-t)/u+120:l=60*(t-n)/u+240,u===0?c=0:f<=.5?c=u/a:c=u/(2-a),[Math.round(l)%360,c,f,i==null?1:i]},o.hsla.from=function(e){if(e[0]==null||e[1]==null||e[2]==null)return[null,null,null,e[3]];var t=e[0]/360,n=e[1],r=e[2],i=e[3],s=r<=.5?r*(1+n):r+n-r*n,o=2*r-s;return[Math.round(d(o,s,t+1/3)*255),Math.round(d(o,s,t)*255),Math.round(d(o,s,t-1/3)*255),i]},c(o,function(n,i){var o=i.props,u=i.cache,a=i.to,f=i.from;s.fn[n]=function(n){a&&!this[u]&&(this[u]=a(this._rgba));if(n===t)return this[u].slice();var r,i=e.type(n),l=i==="array"||i==="object"?n:arguments,p=this[u].slice();return c(o,function(e,t){var n=l[i==="object"?e:t.idx];n==null&&(n=p[t.idx]),p[t.idx]=h(n,t)}),f?(r=s(f(p)),r[u]=p,r):s(p)},c(o,function(t,i){if(s.fn[t])return;s.fn[t]=function(s){var o=e.type(s),u=t==="alpha"?this._hsla?"hsla":"rgba":n,a=this[u](),f=a[i.idx],l;return o==="undefined"?f:(o==="function"&&(s=s.call(this,f),o=e.type(s)),s==null&&i.empty?this:(o==="string"&&(l=r.exec(s),l&&(s=f+parseFloat(l[2])*(l[1]==="+"?1:-1))),a[i.idx]=s,this[u](a)))}})}),s.hook=function(t){var n=t.split(" ");c(n,function(t,n){e.cssHooks[n]={set:function(t,r){var i,o,u="";if(r!=="transparent"&&(e.type(r)!=="string"||(i=p(r)))){r=s(i||r);if(!a.rgba&&r._rgba[3]!==1){o=n==="backgroundColor"?t.parentNode:t;while((u===""||u==="transparent")&&o&&o.style)try{u=e.css(o,"backgroundColor"),o=o.parentNode}catch(f){}r=r.blend(u&&u!=="transparent"?u:"_default")}r=r.toRgbaString()}try{t.style[n]=r}catch(f){}}},e.fx.step[n]=function(t){t.colorInit||(t.start=s(t.elem,n),t.end=s(t.end),t.colorInit=!0),e.cssHooks[n].set(t.elem,t.start.transition(t.end,t.pos))}})},s.hook(n),e.cssHooks.borderColor={expand:function(e){var t={};return c(["Top","Right","Bottom","Left"],function(n,r){t["border"+r+"Color"]=e}),t}},l=e.Color.names={aqua:"#00ffff",black:"#000000",blue:"#0000ff",fuchsia:"#ff00ff",gray:"#808080",green:"#008000",lime:"#00ff00",maroon:"#800000",navy:"#000080",olive:"#808000",purple:"#800080",red:"#ff0000",silver:"#c0c0c0",teal:"#008080",white:"#ffffff",yellow:"#ffff00",transparent:[null,null,null,0],_default:"#ffffff"}}(jQuery),function(){function i(t){var n,r,i=t.ownerDocument.defaultView?t.ownerDocument.defaultView.getComputedStyle(t,null):t.currentStyle,s={};if(i&&i.length&&i[0]&&i[i[0]]){r=i.length;while(r--)n=i[r],typeof i[n]=="string"&&(s[e.camelCase(n)]=i[n])}else for(n in i)typeof i[n]=="string"&&(s[n]=i[n]);return s}function s(t,n){var i={},s,o;for(s in n)o=n[s],t[s]!==o&&!r[s]&&(e.fx.step[s]||!isNaN(parseFloat(o)))&&(i[s]=o);return i}var n=["add","remove","toggle"],r={border:1,borderBottom:1,borderColor:1,borderLeft:1,borderRight:1,borderTop:1,borderWidth:1,margin:1,padding:1};e.each(["borderLeftStyle","borderRightStyle","borderBottomStyle","borderTopStyle"],function(t,n){e.fx.step[n]=function(e){if(e.end!=="none"&&!e.setAttr||e.pos===1&&!e.setAttr)jQuery.style(e.elem,n,e.end),e.setAttr=!0}}),e.fn.addBack||(e.fn.addBack=function(e){return this.add(e==null?this.prevObject:this.prevObject.filter(e))}),e.effects.animateClass=function(t,r,o,u){var a=e.speed(r,o,u);return this.queue(function(){var r=e(this),o=r.attr("class")||"",u,f=a.children?r.find("*").addBack():r;f=f.map(function(){var t=e(this);return{el:t,start:i(this)}}),u=function(){e.each(n,function(e,n){t[n]&&r[n+"Class"](t[n])})},u(),f=f.map(function(){return this.end=i(this.el[0]),this.diff=s(this.start,this.end),this}),r.attr("class",o),f=f.map(function(){var t=this,n=e.Deferred(),r=e.extend({},a,{queue:!1,complete:function(){n.resolve(t)}});return this.el.animate(this.diff,r),n.promise()}),e.when.apply(e,f.get()).done(function(){u(),e.each(arguments,function(){var t=this.el;e.each(this.diff,function(e){t.css(e,"")})}),a.complete.call(r[0])})})},e.fn.extend({_addClass:e.fn.addClass,addClass:function(t,n,r,i){return n?e.effects.animateClass.call(this,{add:t},n,r,i):this._addClass(t)},_removeClass:e.fn.removeClass,removeClass:function(t,n,r,i){return arguments.length>1?e.effects.animateClass.call(this,{remove:t},n,r,i):this._removeClass.apply(this,arguments)},_toggleClass:e.fn.toggleClass,toggleClass:function(n,r,i,s,o){return typeof r=="boolean"||r===t?i?e.effects.animateClass.call(this,r?{add:n}:{remove:n},i,s,o):this._toggleClass(n,r):e.effects.animateClass.call(this,{toggle:n},r,i,s)},switchClass:function(t,n,r,i,s){return e.effects.animateClass.call(this,{add:n,remove:t},r,i,s)}})}(),function(){function r(t,n,r,i){e.isPlainObject(t)&&(n=t,t=t.effect),t={effect:t},n==null&&(n={}),e.isFunction(n)&&(i=n,r=null,n={});if(typeof n=="number"||e.fx.speeds[n])i=r,r=n,n={};return e.isFunction(r)&&(i=r,r=null),n&&e.extend(t,n),r=r||n.duration,t.duration=e.fx.off?0:typeof r=="number"?r:r in e.fx.speeds?e.fx.speeds[r]:e.fx.speeds._default,t.complete=i||n.complete,t}function i(t){return!t||typeof t=="number"||e.fx.speeds[t]?!0:typeof t=="string"&&!e.effects.effect[t]}e.extend(e.effects,{version:"1.10.1",save:function(e,t){for(var r=0;r<t.length;r++)t[r]!==null&&e.data(n+t[r],e[0].style[t[r]])},restore:function(e,r){var i,s;for(s=0;s<r.length;s++)r[s]!==null&&(i=e.data(n+r[s]),i===t&&(i=""),e.css(r[s],i))},setMode:function(e,t){return t==="toggle"&&(t=e.is(":hidden")?"show":"hide"),t},getBaseline:function(e,t){var n,r;switch(e[0]){case"top":n=0;break;case"middle":n=.5;break;case"bottom":n=1;break;default:n=e[0]/t.height}switch(e[1]){case"left":r=0;break;case"center":r=.5;break;case"right":r=1;break;default:r=e[1]/t.width}return{x:r,y:n}},createWrapper:function(t){if(t.parent().is(".ui-effects-wrapper"))return t.parent();var n={width:t.outerWidth(!0),height:t.outerHeight(!0),"float":t.css("float")},r=e("<div></div>").addClass("ui-effects-wrapper").css({fontSize:"100%",background:"transparent",border:"none",margin:0,padding:0}),i={width:t.width(),height:t.height()},s=document.activeElement;try{s.id}catch(o){s=document.body}return t.wrap(r),(t[0]===s||e.contains(t[0],s))&&e(s).focus(),r=t.parent(),t.css("position")==="static"?(r.css({position:"relative"}),t.css({position:"relative"})):(e.extend(n,{position:t.css("position"),zIndex:t.css("z-index")}),e.each(["top","left","bottom","right"],function(e,r){n[r]=t.css(r),isNaN(parseInt(n[r],10))&&(n[r]="auto")}),t.css({position:"relative",top:0,left:0,right:"auto",bottom:"auto"})),t.css(i),r.css(n).show()},removeWrapper:function(t){var n=document.activeElement;return t.parent().is(".ui-effects-wrapper")&&(t.parent().replaceWith(t),(t[0]===n||e.contains(t[0],n))&&e(n).focus()),t},setTransition:function(t,n,r,i){return i=i||{},e.each(n,function(e,n){var s=t.cssUnit(n);s[0]>0&&(i[n]=s[0]*r+s[1])}),i}}),e.fn.extend({effect:function(){function o(n){function u(){e.isFunction(i)&&i.call(r[0]),e.isFunction(n)&&n()}var r=e(this),i=t.complete,o=t.mode;(r.is(":hidden")?o==="hide":o==="show")?u():s.call(r[0],t,u)}var t=r.apply(this,arguments),n=t.mode,i=t.queue,s=e.effects.effect[t.effect];return e.fx.off||!s?n?this[n](t.duration,t.complete):this.each(function(){t.complete&&t.complete.call(this)}):i===!1?this.each(o):this.queue(i||"fx",o)},_show:e.fn.show,show:function(e){if(i(e))return this._show.apply(this,arguments);var t=r.apply(this,arguments);return t.mode="show",this.effect.call(this,t)},_hide:e.fn.hide,hide:function(e){if(i(e))return this._hide.apply(this,arguments);var t=r.apply(this,arguments);return t.mode="hide",this.effect.call(this,t)},__toggle:e.fn.toggle,toggle:function(t){if(i(t)||typeof t=="boolean"||e.isFunction(t))return this.__toggle.apply(this,arguments);var n=r.apply(this,arguments);return n.mode="toggle",this.effect.call(this,n)},cssUnit:function(t){var n=this.css(t),r=[];return e.each(["em","px","%","pt"],function(e,t){n.indexOf(t)>0&&(r=[parseFloat(n),t])}),r}})}(),function(){var t={};e.each(["Quad","Cubic","Quart","Quint","Expo"],function(e,n){t[n]=function(t){return Math.pow(t,e+2)}}),e.extend(t,{Sine:function(e){return 1-Math.cos(e*Math.PI/2)},Circ:function(e){return 1-Math.sqrt(1-e*e)},Elastic:function(e){return e===0||e===1?e:-Math.pow(2,8*(e-1))*Math.sin(((e-1)*80-7.5)*Math.PI/15)},Back:function(e){return e*e*(3*e-2)},Bounce:function(e){var t,n=4;while(e<((t=Math.pow(2,--n))-1)/11);return 1/Math.pow(4,3-n)-7.5625*Math.pow((t*3-2)/22-e,2)}}),e.each(t,function(t,n){e.easing["easeIn"+t]=n,e.easing["easeOut"+t]=function(e){return 1-n(1-e)},e.easing["easeInOut"+t]=function(e){return e<.5?n(e*2)/2:1-n(e*-2+2)/2}})}()}(jQuery);(function(e,t){e.effects.effect.drop=function(t,n){var r=e(this),i=["position","top","bottom","left","right","opacity","height","width"],s=e.effects.setMode(r,t.mode||"hide"),o=s==="show",u=t.direction||"left",a=u==="up"||u==="down"?"top":"left",f=u==="up"||u==="left"?"pos":"neg",l={opacity:o?1:0},c;e.effects.save(r,i),r.show(),e.effects.createWrapper(r),c=t.distance||r[a==="top"?"outerHeight":"outerWidth"](!0)/2,o&&r.css("opacity",0).css(a,f==="pos"?-c:c),l[a]=(o?f==="pos"?"+=":"-=":f==="pos"?"-=":"+=")+c,r.animate(l,{queue:!1,duration:t.duration,easing:t.easing,complete:function(){s==="hide"&&r.hide(),e.effects.restore(r,i),e.effects.removeWrapper(r),n()}})}})(jQuery);