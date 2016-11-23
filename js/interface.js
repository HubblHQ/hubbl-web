$(document).ready(function() {

    PLAYER_FRAME = '#playlist';
    USER_SETTINGS_FRAME = '#settings';

    USERNAME_BTN = '#username-btn';
    BACK_BTN = '#arrow-back-btn';
    MORE_BTN = '#more-btn';

    class ScreenState {

        constructor(frame) {
            this.curFrame = frame;
        }

        getCurFrame() {
            return this.curFrame;
        }

        setFrame(frame) {

            switch (frame) {
                case PLAYER_FRAME:
                    $(BACK_BTN).hide();
                    $(MORE_BTN).show();
                    break;
                case USER_SETTINGS_FRAME:
                    $(MORE_BTN).hide();
                    $(BACK_BTN).show();
                    break;
                default:
                    alert('No such frame: ' + frame);
                    return;
            }
            $(this.curFrame).hide();
            this.curFrame = frame;
            $(this.curFrame).show();
        }
    }

    var state = new ScreenState(PLAYER_FRAME);

    $(USERNAME_BTN).click(function() {
        if (state.getCurFrame() != USER_SETTINGS_FRAME) {
            state.setFrame(USER_SETTINGS_FRAME);
        }
    });

    $(BACK_BTN).click(function() {
        switch (state.getCurFrame()) {
            case PLAYER_FRAME:
                alert('Cannot go back on player frame!');
                return;
            case USER_SETTINGS_FRAME:
                state.setFrame(PLAYER_FRAME);
                break;
        }
    });

});
