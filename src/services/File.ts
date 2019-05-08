import fs from 'fs';
import path from 'path';

export default class File {
    private static instance: File;

    private content = '';

    private constructor() {}

    static getInstance() {
        if (!File.instance) {
            File.instance = new File();
        }

        return File.instance;
    }

    loadFile(dir: string): Promise<this> {
        return new Promise((resolve, reject) => {

            const pathToFile = path.join(process.cwd(), dir);

            /** Load the content as string */
            fs.readFile(pathToFile, { encoding: 'utf-8' }, (err, content) => {
                if (err) return reject(err);

                this.content = content;

                return resolve(this);
            });
        });
    };

    async parseContent(): Promise<any> {
        try {
            if (!this.content) {
                throw 'Error! Can not process an empty file';
            }

            const data = this.content.split('\n');

            const content: any = {};

            data.map(entry => {
                const entries = entry.split(',');

                if (entries[0].toLowerCase() !== 'name') {
                    content[entries[3]] = {
                        name: entries[0],
                        city: entries[1],
                        country: entries[2],
                    }
                }
            });

            return content;
        } catch (err) {
            throw err;
        }
    }
}
