export abstract class Base64 {

    /**
     * Encode string to base64
     *
     * @export
     * @param str String to encode
     * @returns Encoded string
     */
     public static encode(str: string): string {
        return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, (match, p1) => {
            return String.fromCharCode(('0x' + p1) as any);
        }));
    }

    /**
     * Decode string from base64
     *
     * @export
     * @param srt String to decode
     * @returns Decoded string
     */
    public static decode(str: string): string {
        return decodeURIComponent(Array.prototype.map.call(atob(str), (c) => {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
    }
}
