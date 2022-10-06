use crate::writer::IndentWriter;
use crate::Error;
use std::fs::File;
use std::path::Path;

/// Main entry point for backends to generate language bindings.
///
/// This trait will be implemented by each backend and is the main way to interface with a generator.
pub trait Interop {
    /// Generates FFI binding code and writes them to the [`IndentWriter`].
    fn write_to(&self, w: &mut IndentWriter) -> Result<(), Error>;

    /// Convenience method to write FFI bindings to the specified file with default indentation.
    fn write_file<P: AsRef<Path>>(&self, file_name: P) -> Result<(), Error> {
        let mut file = File::create(file_name)?;
        let mut writer = IndentWriter::new(&mut file);

        self.write_to(&mut writer)
    }
}
