package com.guestlogix.vos;

/**
 * A dumb class to hold values and don't do anything else with it.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

public class KeyValueVo<T, J> {
    private T key;
    private J value;

    public KeyValueVo() {

    }

    public KeyValueVo(T key, J value) {
        this.key = key;
        this.value = value;
    }

    public T getKey() {
        return key;
    }

    public void setKey(T key) {
        this.key = key;
    }

    public J getValue() {
        return value;
    }

    public void setValue(J value) {
        this.value = value;
    }
}
