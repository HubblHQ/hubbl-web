PLAYER_STATE_PLAY = 'play';
PLAYER_STATE_PAUSE = 'pause';
PLAYER_STATE_STOP = 'stop';

class PlayerInterface {
    constructor() {
        this.curPlayingPosition = 0;
        this.curPreloadPosition = 0;
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
}

class PlayListEntry {
    constructor(id, name, author, publisherID, providerURI, duration) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.publisherID = publisherID;
        this.providerURI = providerURI;
        this.duration = duration;
        this.likes = 0;
        this.dislikes = 0;
    }

    constructor(id, name, author, publisherID, providerURI, duration, likes, dislikes) {
        this.id = id;
        this.name = name;
        this.author = author;
        this.publisherID = publisherID;
        this.providerURI = providerURI;
        this.duration = duration;
        this.likes = likes;
        this.dislikes = dislikes;
    }
}


class PlayList {
    constructor() {
        this.list = [];
    }

    add(trackName)
}


class Player {
    constructor(playlist) {
        this.interface = new PlayerInterface();
        this.state = PLAYER_STATE_STOP;
        this.playlist = playlist;
    }

    getState() {
        return this.state;
    }

    play() {
        this.state = PLAYER_STATE_PLAY;
    }

    pause() {
        this.state = PLAYER_STATE_PAUSE;
    }

    stop() {
        this.state = PLAYER_STATE_STOP;
    }

}

$(document).ready(function() {
    var player = new Player();
}
