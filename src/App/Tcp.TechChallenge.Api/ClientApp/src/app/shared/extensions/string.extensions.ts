export class StringExtensions {
    static format(stringFormat: string, args: string[]): string {
        return stringFormat.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match
                ;
        });
    }
}