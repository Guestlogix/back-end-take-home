import File from './File';

describe('File Service', () => {
    test('should throw an error if file not found', async () => {
        try {
            await File.getInstance().loadFile('some/fake/path.csv');
        } catch (error) {
            expect(error.errno).toEqual(-2);
            expect(error.code).toBe('ENOENT');
        }
    });

    test('should load file when supplied with existing file path', async () => {
        const content = await File.getInstance().loadFile('data/airlines.csv');
        expect(content).toBeInstanceOf(File);
    });

    test('should FAIL to parse loaded file content', async () => {
        try {
            const file = await File.getInstance().loadFile('__tests__/empty.csv');
            await file.parseContent();
        } catch (error) {
            expect(error).toBe('Error! Can not process an empty file');
        }
    });

    test('should correctly parse loaded file content', async () => {
        const file = await File.getInstance().loadFile('data/airlines.csv');
        const content = await file.parseContent();
        expect(content).toBeDefined();
    });
});
