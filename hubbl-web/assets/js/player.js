PLAYER_STATE_PLAY = 'play';
PLAYER_STATE_PAUSE = 'pause';
PLAYER_STATE_STOP = 'stop';

PLAYLIST_ENTRY_STATUS_ACCEPTED = 1;
PLAYLIST_ENTRY_STATUS_PENDING = 0;
PLAYLIST_ENTRY_STATUS_REFUSED = -1;

//code.stephenmorley.org
function Queue(){var a=[],b=0;this.getLength=function(){return a.length-b};this.isEmpty=function(){return 0==a.length};this.enqueue=function(b){a.push(b)};this.dequeue=function(){if(0!=a.length){var c=a[b];2*++b>=a.length&&(a=a.slice(b),b=0);return c}};this.peek=function(){return 0<a.length?a[b]:void 0}};

class PlayerInterface {
    constructor(player) {
        this.curPlayingPosition = 0;
        this.curPreloadPosition = 0;
        this.player = player;
    }

    setPlayingPosition(position) {
        if (position < 0 || position > 100) return;
        this.curPlayingPosition = position;
        $(PLAY_BAR).css('width', '' + this.curPlayingPosition + '%');
    }

    setPreloadPosition(position) {
        if (position < 0 || position > 100) return;
        this.curPreloadPosition = position;
        $(PRELOAD_BAR).css('width', '' + this.curPreloadPosition + '%');
    }

    getCurPlayingPosition() {
        return this.curPlayingPosition;
    }

    getCurPreloadPosition() {
        return this.curPreloadPosition;
    }

    setTrackInfo(coverURI, title, author) {
        //$(CUR_COVER).(coverURI);
        $(CUR_TITLE).text(title);
        $(CUR_AUTHOR).text(author);
    }

    play() {
        var track = this.player.playlist.get();
        this.setTrackInfo(track.providerURI, track.name, track.author);
        $(PLAY_BTN).hide();
        $(PAUSE_BTN).show();
    }

    pause() {
        $(PAUSE_BTN).hide();
        $(PLAY_BTN).show();
    }

    stop() {
        $(PAUSE_BTN).hide();
        $(PLAY_BTN).show();
        this.setTrackInfo('', '', '');
        this.setPlayingPosition(0);
        this.setPreloadPosition(0);
    }

    update() {
        switch (this.player.state) {
            case PLAYER_STATE_PLAY:
                var track = this.player.playlist.get();
                this.setPlayingPosition(track.audio.currentTime / track.audio.duration * 100);
                this.setPreloadPosition(track.audio.buffered.end(track.audio.buffered.length-1) / track.audio.duration * 100);
                break;
            case PLAYER_STATE_PAUSE:
            case PLAYER_STATE_STOP:
                break;
        }
    }
}

class PlayListEntry {
    /*
    suddenly powerfull OOP language supports only one constructor
    constructor(id, title, author, publisherID, providerURI, duration) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.publisherID = publisherID;
        this.providerURI = providerURI;
        this.duration = duration;
        this.likes = 0;
        this.dislikes = 0;
        this.status = PLAYLIST_ENTRY_STATUS_PENDING;
    }*/

    constructor(id, name, author, publisherID, providerURI, duration, likes, dislikes, status) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.publisherID = publisherID;
        this.providerURI = providerURI;
        this.duration = duration;
        this.likes = likes;
        this.dislikes = dislikes;
        this.status = status;
        this.audio = new Audio(providerURI);
    }

    compareWith(other) {
        if (this.status < other.status) return -1;
        if (this.status > other.status) return -1;
        if (this.likes - this.dislikes < other.likes - other.dislikes) return -1;
        if (this.likes - this.dislikes > other.likes - other.dislikes) return 1;
        return 0;
    }

}


class PlayList {
    constructor() {
        this.queue = new Queue();
    }

    add(entry) {
        this.queue.enqueue(entry);
        this.sort();
    }

    drop() {
        return this.queue.dequeue();
    }

    get() {
        return this.queue.peek();
    }

    sort() {
        //this.queue.a.sort(function(entryA, entryB){
        //    return entryA.compareWith(entryB);
        //});
    }

    isEmpty() {
        return this.queue.isEmpty();
    }
}


class Player {

    constructor(playlist) {
        this.playlist = playlist;
        this.state = PLAYER_STATE_PAUSE;
        this.isUpdateNeeded = false;
        this.interface = new PlayerInterface(this);
    }

    getState() {
        return this.state;
    }

    getInterface() {
        return this.interface;
    }

    play() {
        switch (this.state) {
            case PLAYER_STATE_PLAY:
                break;
            case PLAYER_STATE_PAUSE:
                if (this.playlist.isEmpty()) {
                    this.state = PLAYER_STATE_STOP;
                    this.interface.stop();
                } else {
                    this.state = PLAYER_STATE_PLAY;
                    this.playlist.get().audio.play();
                    this.interface.play();
                }
                break;
            case PLAYER_STATE_STOP:
                if (this.playlist.isEmpty()) {
                    break;
                } else {
                    this.state = PLAYER_STATE_PLAY;
                    this.playlist.get().audio.play();
                    this.interface.play();
                }
                break;
        }
    }

    pause() {
        this.state = PLAYER_STATE_PAUSE;
        if (!this.playlist.isEmpty()) this.playlist.get().audio.pause();
        this.interface.pause();
    }

    stop() {
        this.state = PLAYER_STATE_STOP;
        this.interface.stop();
    }

    update() {
        switch (this.state) {
            case PLAYER_STATE_PLAY:
                if (this.playlist.get().audio.ended) {
                    this.playlist.drop();
                    if (this.playlist.isEmpty()) this.stop(); else this.play();
                }
                break;
            case PLAYER_STATE_PAUSE:
                break;
            case PLAYER_STATE_STOP:
                if (this.playlist.isEmpty()) return; else this.play();
                break;
        }
        this.interface.update();
    }

}
